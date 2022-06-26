using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Repositories;

public class PlaylistTrackRepository : IPlaylistTrackRepository
{
    private readonly IPlaylistService _playlistService;
    private readonly UserTracksManagementDatabaseContext _databaseContext;

    public PlaylistTrackRepository(
        IPlaylistService playlistService,
        UserTracksManagementDatabaseContext databaseContext)
    {
        _playlistService = playlistService;
        _databaseContext = databaseContext;
    }
    
    public async Task AddAsync(Playlist playlist, Track track, CancellationToken cancellationToken)
    {
        var playlistTrack = _playlistService.AddTrackToPlaylist(playlist, track);
        await _databaseContext.PlaylistTracks.AddAsync(playlistTrack, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Playlist playlist, Track track, CancellationToken cancellationToken)
    {
        var playlistTrack = _playlistService.RemoveTrackFromPlaylist(playlist, track);
        await _databaseContext.PlaylistTracks.AddAsync(playlistTrack, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}