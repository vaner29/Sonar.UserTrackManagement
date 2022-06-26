using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface ICheckAvailabilityService
{
    Task<bool> CheckTrackAvailability(string token, User user, Track track, CancellationToken cancellationToken);
    bool CheckPlaylistAvailability(User userId, Playlist playlist);
    bool IsTrackOwner(User user, Track track);
    bool IsPlaylistOwner(User user, Playlist playlist);


}