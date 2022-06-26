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
    public UserTracksApplicationService(
        IUserTrackService trackService,
        IAuthorizationService trackManagerService,
        ICheckAvailabilityService checkAvailabilityService, 
        ITrackRepository trackRepository)
    {
        _trackService = trackService;
        _authorizationService = trackManagerService;
        _checkAvailabilityService = checkAvailabilityService;
        _trackRepository = trackRepository;
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
        
        return tracks.Select(item => new TrackDto()
        {
            Id = item.Id,
            Name = item.Name
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
            .GetIfAvailableAsync(token, user, trackId, cancellationToken);

        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name
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
            .GetIfOwnerAsync(user, trackId, cancellationToken);
        
        _trackService.ChangeAccessType(track, type);
    }

    public async Task ChangeAccessType(string token, Guid trackId, AccessType type, CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var track = await _context.Tracks
            .FirstOrDefaultAsync(
                item => item.Id.Equals(trackId), 
                cancellationToken: cancellationToken);
        
        if (track is null)
        {
            throw new InvalidArgumentsException("Couldn't find track with given ID");
        }

        if (!await _checkAvailabilityService.CheckTrackAvailability(token, user, track, cancellationToken))
        {
            throw new UserAccessException("User doesn't have access to given track");
        }
        
        _userTracksService.ChangeAccessType(track, type);
    }
}