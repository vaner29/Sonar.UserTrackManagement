using Sonar.UserTracksManagement.Application.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IUserTracksApplicationService
{
    Task<Guid> AddTrackAsync(string token, string name);
    Task<bool> CheckAccessToTrackAsync(string token, Guid trackId);
    Task<IEnumerable<TrackDto>> GetAllTracksAsync(string token);
    Task<TrackDto> GetTrackAsync(string token, Guid trackId);
    Task<TrackDto> DeleteTrackAsync(string token, Guid trackId);
}