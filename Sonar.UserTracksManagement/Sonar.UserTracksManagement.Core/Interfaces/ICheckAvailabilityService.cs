using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface ICheckAvailabilityService
{
    bool CheckAvailability(Guid userId, Track track);
}