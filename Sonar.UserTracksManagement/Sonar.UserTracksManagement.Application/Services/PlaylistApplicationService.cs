using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Application.Database;
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
    public PlaylistApplicationService(IPlaylistService playlistService, IAuthorizationService authorizationService, UserTracksManagementDatabaseContext databaseContext, ICheckAvailabilityService availabilityService)
    {
        _playlistService = playlistService;
        _authorizationService = authorizationService;
        _databaseContext = databaseContext;
        _availabilityService = availabilityService;
    }
    public async Task<Guid> CreateAsync(string token, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidArgumentsException("Name can't be empty or contain only whitespaces");
        var user = await _authorizationService.GetUserAsync(token);
        return _playlistService.CreateNewPlaylist(user.Id, name).Id;
    }

    public async Task AddTrackAsync(string token, Guid playlistId, Guid trackId)
    {
        if (playlistId.Equals(Guid.Empty) || trackId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        var user = await _authorizationService.GetUserAsync(token);
        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(item => item.Id.Equals(playlistId));
        var track = await _databaseContext.Tracks.FirstOrDefaultAsync(item => item.Id.Equals(trackId));
        if (playlist == null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (track == null)
            throw new NotFoundArgumentsException("Couldn't find track with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user.Id, playlist))
            throw new PreconditionException("User doesn't have access to given playlist");
        if (!_availabilityService.CheckTrackAvailability(user.Id, track))
            throw new PreconditionException("User doesn't have access to given track");
        var playlistTrack = _playlistService.AddTrackToPlaylist(playlist, track);
        await _databaseContext.PlaylistTracks.AddAsync(playlistTrack);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId)
    {
        if (playlistId.Equals(Guid.Empty) || trackId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        var user = await _authorizationService.GetUserAsync(token);
        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(item => item.Id.Equals(playlistId));
        var track = await _databaseContext.Tracks.FirstOrDefaultAsync(item => item.Id.Equals(trackId));
        if (playlist == null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (track == null)
            throw new NotFoundArgumentsException("Couldn't find track with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user.Id, playlist))
            throw new PreconditionException("User doesn't have access to given playlist");
        if (!_availabilityService.CheckTrackAvailability(user.Id, track))
            throw new PreconditionException("User doesn't have access to given track");
        if (playlist.Tracks.All(item => item.Track.Id.Equals(trackId)))
            throw new NotFoundArgumentsException("No track with given ID in the playlist");
        var playlistTrack = _playlistService.RemoveTrackFromPlaylist(playlist, track);
        _databaseContext.PlaylistTracks.Remove(playlistTrack);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Track>> GetTracksFromPlaylistAsync(string token, Guid playlistId)
    {
        if (playlistId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");
        var user = await _authorizationService.GetUserAsync(token);
        var playlist = await _databaseContext.Playlists.FirstOrDefaultAsync(item => item.Id.Equals(playlistId));
        if (playlist == null)
            throw new NotFoundArgumentsException("Couldn't find playlist with given ID");
        if (!_availabilityService.CheckPlaylistAvailability(user.Id, playlist))
            throw new PreconditionException("User doesn't have access to given playlist");
        return _playlistService.GetTracksFromPlaylist(playlist);
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token)
    {
        var user = await _authorizationService.GetUserAsync(token);
        return _databaseContext.Playlists.Where(playlist => playlist.UserId.Equals(user.Id));

    }
}