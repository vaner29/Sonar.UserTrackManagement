using Sonar.UserTracksManagement.Application.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IUserTracksApplicationService
{
    Task<Guid> AddTrackAsync(string token, string name);
    Task<bool> CheckAccessToTrackAsync(string token, Guid trackId);
    Task<IEnumerable<TrackDto>> GetAllUserTracksAsync(string token);
    Task<TrackDto> GetTrackAsync(string token, Guid trackId);
    Task DeleteTrackAsync(string token, Guid trackId);
}