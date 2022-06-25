using Sonar.UserProfile.ApiClient.Dto;
using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserProfile.ApiClient.Tools;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.ServiceFaker.Services;

public class FakeAuthorizationService : IAuthorizationService
{
    private readonly IUserApiClient _apiClient;
    public FakeAuthorizationService(IUserApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<Guid> GetUserIdAsync(string token, CancellationToken cancellationToken)
    {
        return Guid.Parse(token);
    }

    public async Task<User> GetUserAsync(string token, CancellationToken cancellationToken)
    {
        return new User(Guid.Parse(token), Guid.Parse(token) + "@abobus.kek", new List<Guid>());
    }
}