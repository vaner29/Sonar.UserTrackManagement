using Sonar.UserProfile.ApiClient.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IAuthorizationService
{
    Task<UserDto> GetUserAsync(string token, CancellationToken cancellationToken);
}