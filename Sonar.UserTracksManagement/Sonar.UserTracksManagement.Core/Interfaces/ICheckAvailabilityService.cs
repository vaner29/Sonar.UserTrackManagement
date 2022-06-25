using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface ICheckAvailabilityService
{
    bool CheckTrackAvailability(User userId, Track track);
    bool CheckPlaylistAvailability(User userId, Playlist playlist);
}