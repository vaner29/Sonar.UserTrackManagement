using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface ICheckAvailabilityService
{
    bool CheckTrackAvailability(User userId, Track track);
    bool CheckPlaylistAvailability(User userId, Playlist playlist);
}