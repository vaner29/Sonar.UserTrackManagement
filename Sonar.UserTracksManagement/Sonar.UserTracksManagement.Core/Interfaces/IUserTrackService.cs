using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface IUserTrackService
{
    Track AddNewTrack(Guid ownerId, string name);
}