using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface ITagApplicationService
{
    Task<Tag> RegisterTagAsync(string name, CancellationToken cancellationToken);
    Task AssignTagToPlaylistAsync(string token, string tag, Guid playlistId, CancellationToken cancellationToken);
    Task AssignTagToTrackAsync(string token, string tag, Guid trackId, CancellationToken cancellationToken);
    Task RemoveTagFromPlaylistAsync(string token, string tag, Guid playlistId, CancellationToken cancellationToken);
    Task RemoveTagFromTrackAsync(string token, string tag, Guid trackId, CancellationToken cancellationToken);
    Task<IEnumerable<Tag>> GetTrackTags(string token, Guid trackId, CancellationToken cancellationToken);
    Task<IEnumerable<Tag>> GetPlaylistTags(string token, Guid playlistId, CancellationToken cancellationToken);
}