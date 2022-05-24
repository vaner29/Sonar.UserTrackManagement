namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> CreateAsync(Guid userId, string name);
    Task AddTrackAsync(Guid userId, Guid playlistId, Guid trackId);
    Task RemoveTrackAsync(Guid userId, Guid playlistId, Guid trackId);
}