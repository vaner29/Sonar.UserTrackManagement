using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.ServiceFaker.Services;

public class FakeCheckAvailabilityService : ICheckAvailabilityService
{
    private readonly IRelationshipApiClient _apiClient;
    public FakeCheckAvailabilityService(IRelationshipApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public Task<bool> CheckTrackAvailability(string token, User user, Track track, CancellationToken cancellationToken)
    {
        return track.TrackMetaDataInfo.AccessType switch
        {
            AccessType.Public => Task.FromResult(true),
            AccessType.Private => Task.FromResult(IsTrackOwner(user, track)),
            AccessType.OnlyFans => Task.FromResult(IsTrackOwner(user, track)),
            _ => throw new NotImplementedException(
                $"Access type {Enum.GetName(track.TrackMetaDataInfo.AccessType)} not implemented yet")
        };
    }

    public bool CheckPlaylistAvailability(User user, Playlist playlist)
    {
        return IsPlaylistOwner(user, playlist);
    }

    public bool IsTrackOwner(User user, Track track)
    {
        return user.UserId == track.OwnerId;
    }

    public bool IsPlaylistOwner(User user, Playlist playlist)
    {
        return playlist.UserId == user.UserId;
    }
}