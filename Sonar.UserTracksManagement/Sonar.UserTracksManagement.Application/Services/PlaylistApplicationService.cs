using Microsoft.EntityFrameworkCore;
using Sonar.UserProfile.ApiClient;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Interfaces;
using Sonar.UserTracksManagement.Core.Services;

namespace Sonar.UserTracksManagement.Application.Services;

public class PlaylistApplicationService : IPlaylistApplicationService
{

    private readonly IPlaylistService _playlistService;
    private readonly IAuthorizationService _authorizationService;
    private readonly UserTracksManagementDatabaseContext _databaseContext;
    public PlaylistApplicationService(IPlaylistService playlistService, IAuthorizationService authorizationService, UserTracksManagementDatabaseContext databaseContext)
    {
        _playlistService = playlistService;
        _authorizationService = authorizationService;
        _databaseContext = databaseContext;
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
        _playlistService.AddTrackToPlaylist(playlist, track);
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
        _playlistService.RemoveTrackFromPlaylist(playlist, track);
    }
}