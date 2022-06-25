using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> CreateAsync(string token, string name, CancellationToken cancellationToken);
    Task AddTrackAsync(string token, Guid playlistId, Guid trackId, CancellationToken cancellationToken);
    Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId, CancellationToken cancellationToken);
    Task<IEnumerable<TrackDto>> GetTracksFromPlaylistAsync(string token, Guid playlistId, CancellationToken cancellationToken);
    Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token, CancellationToken cancellationToken);
    Task<Playlist> GetUserPlaylistAsync(string token, Guid playlistId, CancellationToken cancellationToken);


}