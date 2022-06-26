using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Repositories;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class PlaylistApplicationService : IPlaylistApplicationService
{
    private readonly IPlaylistService _playlistService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IPlaylistTrackRepository _playlistTrackRepository;
    public PlaylistApplicationService(
        IPlaylistService playlistService,
        IAuthorizationService authorizationService, 
        IPlaylistRepository playlistRepository, 
        ITrackRepository trackRepository, 
        IPlaylistTrackRepository playlistTrackRepository)
    {
        _playlistService = playlistService;
        _authorizationService = authorizationService;
        _playlistRepository = playlistRepository;
        _trackRepository = trackRepository;
        _playlistTrackRepository = playlistTrackRepository;
    }
    public async Task<Guid> CreateAsync(
        string token, 
        string name, 
        CancellationToken cancellationToken)
    {
        var userId = await _authorizationService
            .GetUserIdAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .AddAsync(userId, name, cancellationToken);
        return playlist.Id;
    }

    public async Task AddTrackAsync(
        string token, 
        Guid playlistId, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfAvailableAsync(token, user, playlistId, cancellationToken);
        var track = await _trackRepository
            .GetIfAvailableAsync(token, user, trackId, cancellationToken);
        await _playlistTrackRepository
            .AddAsync(playlist, track, cancellationToken);
    }

    public async Task RemoveTrackAsync(
        string token, 
        Guid playlistId, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfAvailableAsync(token, user, playlistId, cancellationToken);
        var track = await _trackRepository
            .GetIfAvailableAsync(token, user, trackId, cancellationToken);
        if (!_playlistService.CheckPlaylistForTrack(playlist, track))
        {
            throw new PreconditionException("Playlist doesn't have given track");
        }
        
        await _playlistTrackRepository
            .DeleteAsync(playlist, track, cancellationToken);
    }

    public async Task<IEnumerable<TrackDto>> GetTracksFromPlaylistAsync(
        string token,
        Guid playlistId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfAvailableAsync(token, user, playlistId, cancellationToken);
        return _playlistService.GetTracksFromPlaylist(playlist).Select(track => new TrackDto()
        {
            Id = track.Id,
            Name = track.Name
        });
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token, CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        return await _playlistRepository
            .GetUserAllAsync(user, cancellationToken);
    }

    public async Task<Playlist> GetUserPlaylistAsync(string token, Guid playlistId, CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfAvailableAsync(token, user, playlistId, cancellationToken);

        return playlist;
    }
}