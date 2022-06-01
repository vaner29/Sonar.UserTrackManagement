using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> CreateAsync(string token, string name);
    Task AddTrackAsync(string token, Guid playlistId, Guid trackId);
    Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId);
    Task<IEnumerable<Track>> GetTracksFromPlaylistAsync(string token, Guid playlistId);
    Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token);
}