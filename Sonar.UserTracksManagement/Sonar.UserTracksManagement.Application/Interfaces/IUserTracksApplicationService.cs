using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IUserTracksApplicationService
{
    Task<Guid> AddTrack(Guid userId, string name);
    Task<bool> CheckAccessToTrack(Guid userId, Guid trackId);
    Task<List<Guid>> GetAllTracks(Guid userId);
}