namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> Create(Guid userId, string name);
    Task AddTrack(Guid userId, Guid playlistId, Guid trackId);
    Task RemoveTrack(Guid userId, Guid playlistId, Guid trackId);
}