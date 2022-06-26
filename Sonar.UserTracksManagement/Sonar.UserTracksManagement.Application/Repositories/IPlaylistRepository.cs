using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public interface IPlaylistRepository
{
    Task<Playlist> AddAsync(Guid userId, string name, CancellationToken cancellationToken);
    Task<Playlist> GetToAvailableUserAsync(string token, User user, Guid playlistId, CancellationToken cancellationToken);
    Task<Playlist> GetToOwnerAsync(User user, Guid playlistId, CancellationToken cancellationToken);
    Task<Playlist> GetAsync(Guid playlistId, CancellationToken cancellationToken);
    Task DeleteAsync(User user, Guid playlistId, CancellationToken cancellationToken);
    Task<IEnumerable<Playlist>> GetUserAllAsync(User user, CancellationToken cancellationToken);
    Task<IEnumerable<Playlist>> GetUserWithTagAsync(User user, Tag tag, CancellationToken cancellationToken);
}