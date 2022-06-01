using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface ICheckAvailabilityService
{
    bool CheckTrackAvailability(Guid userId, Track track);
    bool CheckPlaylistAvailability(Guid userId, Playlist playlist);
}