using Sonar.UserProfile.ApiClient.Dto;
using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserProfile.ApiClient.Tools;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;

namespace Sonar.UserTracksManagement.ServiceFaker.Services;

public class FakeAuthorizationService : IAuthorizationService
{
    private readonly IUserApiClient _apiClient;
    public FakeAuthorizationService(IUserApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<UserDto> GetUserAsync(string token, CancellationToken cancellationToken)
    {
        return new UserDto
        {
            Email = Guid.Parse(token) + "@abobus.sus",
            Id = Guid.Parse(token)
        };
    }
}