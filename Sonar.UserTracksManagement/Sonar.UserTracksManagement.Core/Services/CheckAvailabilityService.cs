using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class CheckAvailabilityService : ICheckAvailabilityService
{
    public bool CheckAvailability(Guid userId, Track track)
    {
        return track.OwnerId == userId;
    }
}