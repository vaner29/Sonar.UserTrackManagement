using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly IPlaylistService _playlistService;
    private readonly ICheckAvailabilityService _availabilityService;
    private readonly UserTracksManagementDatabaseContext _databaseContext;

    public PlaylistRepository(
        IPlaylistService playlistService,
        ICheckAvailabilityService availabilityService,
        UserTracksManagementDatabaseContext databaseContext)
    {
        _playlistService = playlistService;
        _availabilityService = availabilityService;
        _databaseContext = databaseContext;
    }
    
     public async Task<Playlist> AddAsync(
        Guid userId, 
        string name, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidArgumentsException("Name can't be empty or contain only whitespaces");
        }

        var playlist = _playlistService.CreateNewPlaylist(userId, name);
        await _databaseContext.Playlists.AddAsync(playlist, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        
        return playlist;
    }
    public async Task<Playlist> GetIfAvailableAsync(
        string token, 
        User user, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var playlist = await GetAsync(playlistId, cancellationToken);
        if (!_availabilityService.CheckPlaylistAvailability(user, playlist))
        {
            throw new UserAccessException("User doesn't have access to given playlist");
        }

        return playlist;
    }

    public async Task<Playlist> GetIfOwnerAsync(
        User user, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var playlist = await GetAsync(playlistId, cancellationToken);
        if (!_availabilityService.IsPlaylistOwner(user, playlist))
        {
            throw new UserAccessException("User isn't owner of given track");
        }

        return playlist;
    }

    public async Task DeleteAsync(
        User user, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var playlist = GetIfOwnerAsync(user, playlistId, cancellationToken);
        _databaseContext.Remove(playlist);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public Task<IEnumerable<Playlist>> GetUserAllAsync(
        User user, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _databaseContext.Playlists
                .AsEnumerable()
                .Where(playlist => _availabilityService
                    .CheckPlaylistAvailability(user, playlist)));
    }

   public async Task<Playlist> GetAsync(
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        if (playlistId.Equals(Guid.Empty))
        {
            throw new InvalidArgumentsException("Guid can't be empty");
        }
        
        var playlist = await _databaseContext.Playlists
            .FirstOrDefaultAsync(
                track => track.Id.Equals(playlistId), 
                cancellationToken: cancellationToken);
        
        if (playlist is null)
        {
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        }

        return playlist;
    }
}