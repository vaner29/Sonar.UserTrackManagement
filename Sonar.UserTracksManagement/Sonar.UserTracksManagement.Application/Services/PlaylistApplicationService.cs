using Microsoft.EntityFrameworkCore;
using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class PlaylistApplicationService : IPlaylistApplicationService
{
    private readonly IPlaylistService _playlistService;
    private readonly ICheckAvailabilityService _availabilityService;
    private readonly IAuthorizationService _authorizationService;
    private readonly UserTracksManagementDatabaseContext _databaseContext;
    private readonly IRelationshipApiClient _apiClient;
    public PlaylistApplicationService(
        IRelationshipApiClient apiClient,
        IPlaylistService playlistService,
        IAuthorizationService authorizationService,
        UserTracksManagementDatabaseContext databaseContext,
        ICheckAvailabilityService availabilityService)
    {
        _apiClient = apiClient;
        _playlistService = playlistService;
        _authorizationService = authorizationService;
        _databaseContext = databaseContext;
        _availabilityService = availabilityService;
    }
    public async Task<Guid> CreateAsync(string token, string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidArgumentsException("Name can't be empty or contain only whitespaces");
        
        var userId = await _authorizationService.GetUserIdAsync(token, cancellationToken);
        var playlist = _playlistService.CreateNewPlaylist(userId, name);
        
        await _databaseContext.Playlists.AddAsync(playlist, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
        
        return playlist.Id;
    }

    public async Task AddTrackAsync(string token, Guid playlistId, Guid trackId, CancellationToken cancellationToken)
    {
        if (playlistId.Equals(Guid.Empty) || trackId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(item 
            => item.Id.Equals(playlistId), cancellationToken: cancellationToken);
        var track = await _databaseContext.Tracks.FirstOrDefaultAsync(item
            => item.Id.Equals(trackId), cancellationToken: cancellationToken);
        
        if (playlist is null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (track is null)
            throw new NotFoundArgumentsException("Couldn't find track with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user, playlist))
            throw new UserAccessException("User doesn't have access to given playlist");
        if (!_availabilityService.CheckTrackAvailability(user, track))
            throw new UserAccessException("User doesn't have access to given track");
        
        var playlistTrack = _playlistService.AddTrackToPlaylist(playlist, track);
        
        await _databaseContext.PlaylistTracks.AddAsync(playlistTrack, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId, CancellationToken cancellationToken)
    {
        if (playlistId.Equals(Guid.Empty) || trackId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(item 
            => item.Id.Equals(playlistId), cancellationToken: cancellationToken);
        var track = await _databaseContext.Tracks.FirstOrDefaultAsync(item 
            => item.Id.Equals(trackId), cancellationToken: cancellationToken);
        
        if (playlist is null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (track is null)
            throw new NotFoundArgumentsException("Couldn't find track with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user, playlist))
            throw new UserAccessException("User doesn't have access to given playlist");
        if (!_availabilityService.CheckTrackAvailability(user, track))
            throw new UserAccessException("User doesn't have access to given track");
        if (playlist.Tracks.All(item => item.Track.Id.Equals(trackId)))
            throw new NotFoundArgumentsException("No track with given ID in the playlist");
        
        var playlistTrack = _playlistService.RemoveTrackFromPlaylist(playlist, track);
        
        _databaseContext.PlaylistTracks.Remove(playlistTrack);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TrackDto>> GetTracksFromPlaylistAsync(
        string token,
        Guid playlistId,
        CancellationToken cancellationToken)
    {
        if (playlistId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(
            item => item.Id.Equals(playlistId), cancellationToken: cancellationToken);
        
        if (playlist is null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user, playlist))
            throw new UserAccessException("User doesn't have access to given playlist");
        
        return _playlistService.GetTracksFromPlaylist(playlist).Select(track => new TrackDto()
        {
            Id = track.Id,
            Name = track.Name
        });
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token, CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        return _databaseContext.Playlists
            .AsEnumerable()
            .Where(playlist => _availabilityService
                .CheckPlaylistAvailability(user, playlist));

    }

    public async Task<Playlist> GetUserPlaylistAsync(string token, Guid playlistId, CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(
            p => p.Id.Equals(playlistId), cancellationToken: cancellationToken);
        
        if (playlist == null) throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        
        if (!_availabilityService.CheckPlaylistAvailability(user, playlist))
            throw new UserAccessException("User doesn't have access to given playlist");
        
        return playlist;
    }
}