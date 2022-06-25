using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class CheckAvailabilityService : ICheckAvailabilityService
{
    public bool CheckTrackAvailability(User user, Track track)
    {
        return track.TrackMetaDataInfo.AccessType switch
        {
            AccessType.Public => true,
            AccessType.Private => user.UserId == track.OwnerId,
            AccessType.OnlyFans => user.Friends.Contains(track.OwnerId),
            _ => throw new NotImplementedException(
                $"Access type {Enum.GetName(track.TrackMetaDataInfo.AccessType)} not implemented yet")
        };
    }

    public bool CheckPlaylistAvailability(User user, Playlist playlist)
    {
        return playlist.UserId == user.UserId;
    }
}