using Sonar.UserTracksManagement.Application.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IUserTracksApplicationService
{
    Task<Guid> AddTrackAsync(Guid userId, string name);
    Task<bool> CheckAccessToTrackAsync(Guid userId, Guid trackId);
    Task<IEnumerable<TrackDto>> GetAllTracksAsync(Guid userId);
    Task<TrackDto> GetTrackAsync(Guid userId, Guid trackId);
}