using Sonar.UserProfile.ApiClient;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IAuthorizationService
{
    Task<UserGetDto> GetUserAsync(string token);
}