using Sonar.UserProfile.ApiClient;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;

namespace Sonar.UserTracksManagement.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserApiClient _apiClient;
    public AuthorizationService(IUserApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<UserGetDto> GetUserAsync(string token)
    {
        try
        {
            var userDto = await _apiClient.GetAsync(token);
            return userDto;
        }
        catch (ApiException e)
        {
            throw new UserAuthorizationException(e.Message);
        }
    }
}