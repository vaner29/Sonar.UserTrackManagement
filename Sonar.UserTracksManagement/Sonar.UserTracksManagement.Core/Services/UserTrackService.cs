using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class UserTrackService : IUserTrackService
{
    public Track AddNewTrack(Guid ownerId, string name)
    {
        return new Track(ownerId, name);
    }
}