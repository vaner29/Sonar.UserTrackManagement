using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IAuthorizationService
{
    Task<Guid> GetUserIdAsync(string token, CancellationToken cancellationToken);
    Task<User> GetUserAsync(string token, CancellationToken cancellationToken);
}