using Sonar.UserProfile.ApiClient.Dto;
using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserProfile.ApiClient.Tools;
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

    public async Task<UserDto> GetUserAsync(string token, CancellationToken cancellationToken)
    {
        try
        {
            var userDto = await _apiClient.GetAsync(token, cancellationToken);
            return userDto;
        }
        catch (ApiClientException e)
        {
            throw new UserAuthorizationException(e.Message);
        }
    }
}