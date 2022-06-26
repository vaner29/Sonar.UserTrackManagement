using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Repositories;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class UserTracksApplicationService : IUserTracksApplicationService
{
    private readonly IUserTrackService _trackService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ICheckAvailabilityService _checkAvailabilityService;
    private readonly ITrackRepository _trackRepository;
    private readonly ITagRepository _tagRepository;

    public UserTracksApplicationService(
        IUserTrackService trackService,
        IAuthorizationService trackManagerService,
        ICheckAvailabilityService checkAvailabilityService,
        ITrackRepository trackRepository, 
        ITagRepository tagRepository)
    {
        _trackService = trackService;
        _authorizationService = trackManagerService;
        _checkAvailabilityService = checkAvailabilityService;
        _trackRepository = trackRepository;
        _tagRepository = tagRepository;
    }

    public async Task<Guid> AddTrackAsync(
        string token,
        string name,
        CancellationToken cancellationToken)
    {
        var userId = await _authorizationService
            .GetUserIdAsync(token, cancellationToken);
        var track = await _trackRepository
            .AddAsync(userId, name, cancellationToken);
        return track.Id;
    }

    public async Task<bool> CheckAccessToTrackAsync(
        string token,
        Guid trackId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var track = await _trackRepository
            .GetAsync(trackId, cancellationToken);
        return await _checkAvailabilityService
            .CheckTrackAvailability(token, user, track, cancellationToken);
    }

    public async Task<IEnumerable<TrackDto>> GetAllUserTracksAsync(
        string token,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tracks = await _trackRepository
            .GetUserAllAsync(token, user, cancellationToken);

        return tracks.Select(track => new TrackDto()
        {
            Id = track.Id,
            Name = track.Name,
            OwnerId = track.OwnerId,
            Type = track.TrackMetaDataInfo.AccessType
        });
    }

    public async Task<TrackDto> GetTrackAsync(
        string token,
        Guid trackId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var track = await _trackRepository
            .GetToAvailableUserAsync(token, user, trackId, cancellationToken);

        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name,
            OwnerId = track.OwnerId,
            Type = track.TrackMetaDataInfo.AccessType
        };
    }

    public async Task DeleteTrackAsync(
        string token,
        Guid trackId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        await _trackRepository
            .DeleteAsync(user, trackId, cancellationToken);
    }

    public async Task ChangeAccessType(
        string token,
        Guid trackId,
        AccessType type,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var track = await _trackRepository
            .GetToOwnerAsync(user, trackId, cancellationToken);

        _trackService.ChangeAccessType(track, type);
        await _trackRepository.ChangeTrackAccessLevelAsync(trackId, type, cancellationToken);
    }

    public async Task<IEnumerable<TrackDto>> GetUserTracksWithTagAsync(
        string token, 
        string tagName, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService
            .GetUserAsync(token, cancellationToken);
        var tag = await _tagRepository
            .GetAsync(tagName, cancellationToken);
        var tracks = await _trackRepository
            .GetTrackWithTagForAvailableUserAsync(token, user, tag, cancellationToken);

        return tracks.Select(track => new TrackDto()
        {
            Id = track.Id,
            Name = track.Name,
            OwnerId = track.OwnerId,
            Type = track.TrackMetaDataInfo.AccessType
        });
    }
}