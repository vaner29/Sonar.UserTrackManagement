using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public interface ITrackRepository
{
    Task<Track> AddAsync(Guid userId, string name, CancellationToken cancellationToken);
    Task<Track> GetIfAvailableAsync(string token, User user, Guid trackId, CancellationToken cancellationToken);
    Task<Track> GetIfOwnerAsync(User user, Guid trackId, CancellationToken cancellationToken);
    Task DeleteAsync(User user, Guid trackId, CancellationToken cancellationToken);
    Task<IEnumerable<Track>> GetUserAllAsync(string token, User user, CancellationToken cancellationToken);
    Task<Track> GetAsync(Guid trackId, CancellationToken cancellationToken);
}