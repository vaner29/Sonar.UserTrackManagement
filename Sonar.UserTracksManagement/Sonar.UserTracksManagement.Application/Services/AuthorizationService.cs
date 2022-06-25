using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserProfile.ApiClient.Tools;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserApiClient _apiClient;
    private readonly IRelationshipApiClient _relationshipApiClient;
    public AuthorizationService(IUserApiClient apiClient, IRelationshipApiClient relationshipApiClient)
    {
        _apiClient = apiClient;
        _relationshipApiClient = relationshipApiClient;
    }

    public async Task<Guid> GetUserIdAsync(string token, CancellationToken cancellationToken)
    {
        try
        {
            var userDto = await _apiClient.GetAsync(token, cancellationToken);
            return userDto.Id;
        }
        catch (ApiClientException e)
        {
            throw new UserAuthorizationException(e.Message);
        }
    }

    public async Task<User> GetUserAsync(string token, CancellationToken cancellationToken)
    {
        try
        {
            var userDto = await _apiClient.GetAsync(token, cancellationToken);
            var friends = await _relationshipApiClient.GetFriendsAsync(token, cancellationToken);
            return new User(userDto.Id, userDto.Email, friends.Select(item => item.Id).ToList());
        }
        catch (ApiClientException e)
        {
            throw new UserAuthorizationException(e.Message);
        }
    }
}