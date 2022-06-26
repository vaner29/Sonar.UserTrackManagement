using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IUserTracksApplicationService
{
    Task<Guid> AddTrackAsync(string token, string name, CancellationToken cancellationToken);
    Task<bool> CheckAccessToTrackAsync(string token, Guid trackId, CancellationToken cancellationToken);
    Task<IEnumerable<TrackDto>> GetAllUserTracksAsync(string token, CancellationToken cancellationToken);
    Task<TrackDto> GetTrackAsync(string token, Guid trackId, CancellationToken cancellationToken);
    Task DeleteTrackAsync(string token, Guid trackId, CancellationToken cancellationToken);
    Task ChangeAccessType(string token, Guid trackId, AccessType type, CancellationToken cancellationToken);
}