namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> CreateAsync(string token, string name);
    Task AddTrackAsync(string token, Guid playlistId, Guid trackId);
    Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId);
}