using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class CheckAvailabilityService : ICheckAvailabilityService
{
    public bool CheckTrackAvailability(Guid userId, Track track)
    {
        return track.TrackMetaDataInfo.AccessType switch
        {
            AccessType.Private => track.OwnerId == userId,
            AccessType.Public => true,
            AccessType.OnlyFans => throw new NotImplementedException(
                "support for access level AccessType.OnlyFans not implemented yet"),
            _ => throw new NotImplementedException(
                "unknown access type"),
        };
    }

    public bool CheckPlaylistAvailability(Guid userId, Playlist playlist)
    {
        return playlist.UserId == userId;
    }
}