using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class CheckAvailabilityService : ICheckAvailabilityService
{
    public bool CheckTrackAvailability(Guid userId, Track track)
    {
        return track.OwnerId == userId;
    }

    public bool CheckPlaylistAvailability(Guid userId, Playlist playlist)
    {
        return playlist.UserId == userId;
    }
}