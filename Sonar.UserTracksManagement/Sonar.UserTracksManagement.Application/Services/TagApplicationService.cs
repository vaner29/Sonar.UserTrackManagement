using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Repositories;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class TagApplicationService : ITagApplicationService
{
    private readonly ITagService _tagService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly ITagRepository _tagRepository;


    public TagApplicationService(
        ITagService tagService,
        IAuthorizationService authorizationService,
        ITrackRepository trackRepository, 
        IPlaylistRepository playlistRepository, 
        ITagRepository tagRepository)
    {
        _tagService = tagService;
        _authorizationService = authorizationService;
        _trackRepository = trackRepository;
        _playlistRepository = playlistRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Tag> RegisterTagAsync(
        string name, 
        CancellationToken cancellationToken)
    {
        return await _tagRepository
            .AddAsync(name, cancellationToken);
    }

    public async Task AssignTagToPlaylistAsync(
        string token, 
        string tagName, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tag = await _tagRepository
            .GetAsync(tagName, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfOwnerAsync(user, playlistId, cancellationToken);
        if (playlist.PlaylistMetaDataInfo.Tags.Contains(tag))
        {
            throw new PreconditionException("Playlist already has given tag");
        }
        
        _tagService.AssignTagToMetaDataInfo(playlist.PlaylistMetaDataInfo, tag);
    }

    public async Task AssignTagToTrackAsync(
        string token, 
        string tagName, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tag = await _tagRepository
            .GetAsync(tagName, cancellationToken);
        var track = await _trackRepository
            .GetIfOwnerAsync(user, trackId, cancellationToken);
        if (track.TrackMetaDataInfo.Tags.Contains(tag))
        {
            throw new PreconditionException("Track already has given tag");
        }
        
        _tagService.AssignTagToMetaDataInfo(track.TrackMetaDataInfo, tag);
    }

    public async Task RemoveTagFromPlaylistAsync(
        string token, 
        string tagName, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tag = await _tagRepository
            .GetAsync(tagName, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfOwnerAsync(user, playlistId, cancellationToken);
        if (!playlist.PlaylistMetaDataInfo.Tags.Contains(tag))
        {
            throw new PreconditionException("Playlist doesn't have given tag");
        }
        
        _tagService.RemoveTagFromMetaDataInfo(playlist.PlaylistMetaDataInfo, tag);
    }

    public async  Task RemoveTagFromTrackAsync(
        string token, 
        string tagName, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tag = await _tagRepository
            .GetAsync(tagName, cancellationToken);
        var track = await _trackRepository
            .GetIfOwnerAsync(user, trackId, cancellationToken);
        if (!track.TrackMetaDataInfo.Tags.Contains(tag))
        {
            throw new PreconditionException("Track doesn't have given tag");
        }
        
        _tagService.RemoveTagFromMetaDataInfo(track.TrackMetaDataInfo, tag);
    }

    public async Task<IEnumerable<Tag>> GetTrackTags(string token, Guid trackId, CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var track = await _trackRepository
            .GetIfAvailableAsync(token, user, trackId, cancellationToken);

        return track.TrackMetaDataInfo.Tags;
    }

    public async Task<IEnumerable<Tag>> GetPlaylistTags(string token, Guid playlistId, CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository
            .GetIfAvailableAsync(token, user, playlistId, cancellationToken);
        
        return playlist.PlaylistMetaDataInfo.Tags;
    }
}