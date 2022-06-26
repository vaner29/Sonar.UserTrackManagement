using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public interface IPlaylistTrackRepository
{
    Task AddAsync(
        Playlist playlist, 
        Track track, 
        CancellationToken cancellationToken);
    Task DeleteAsync(
        Playlist playlist, 
        Track track, 
        CancellationToken cancellationToken);
}