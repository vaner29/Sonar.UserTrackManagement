using Microsoft.EntityFrameworkCore;
using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class UserTracksApplicationService : IUserTracksApplicationService
{
    private readonly UserTracksManagementDatabaseContext _context;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserTrackService _userTracksService;
    private readonly ICheckAvailabilityService _checkAvailabilityService;
    private readonly IRelationshipApiClient _apiClient;
    public UserTracksApplicationService(
        IRelationshipApiClient apiClient,
        IAuthorizationService trackManagerService, 
        UserTracksManagementDatabaseContext context, 
        IUserTrackService userTracksService,
        ICheckAvailabilityService checkAvailabilityService)
    {
        _apiClient = apiClient;
        _authorizationService = trackManagerService;
        _context = context;
        _userTracksService = userTracksService;
        _checkAvailabilityService = checkAvailabilityService;
    }

    public async Task<Guid> AddTrackAsync(string token, string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidArgumentsException("Name can't be empty or contain only whitespaces");
        
        var userId = await _authorizationService.GetUserIdAsync(token, cancellationToken);
        var track = _userTracksService.AddNewTrack(userId, name);
        
        await _context.Tracks.AddAsync(track, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return track.Id;
    }

    public async Task<bool> CheckAccessToTrackAsync(string token, Guid trackId, CancellationToken cancellationToken)
    {
        if (trackId.Equals(Guid.Empty))
            throw new InvalidArgumentsException("Guid can't be empty");

        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var track = await _context.Tracks.FirstOrDefaultAsync(
            item => item.Id.Equals(trackId), cancellationToken: cancellationToken);

        if (track is null)
        {
            throw new InvalidArgumentsException("track with given id doesn't exists");
        }
        
        return _checkAvailabilityService.CheckTrackAvailability(user, track);
    }

    public async Task<IEnumerable<TrackDto>> GetAllUserTracksAsync(string token, CancellationToken cancellationToken)
    {
        var userId = await _authorizationService.GetUserIdAsync(token, cancellationToken);
        var tracks = _context.Tracks.Where(item => item.OwnerId.Equals(userId)).ToList();
        
        return tracks.Select(item => new TrackDto()
        {
            Id = item.Id,
            Name = item.Name
        });
    }

    public async Task<TrackDto> GetTrackAsync(string token, Guid trackId, CancellationToken cancellationToken)
    {
        if (trackId.Equals(Guid.Empty))
        {
            throw new InvalidArgumentsException("Guid can't be empty");   
        }

        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var track = await _context.Tracks.FirstOrDefaultAsync(
            item => item.Id.Equals(trackId), cancellationToken: cancellationToken);
        
        if (track is null)
        {
            throw new InvalidArgumentsException("track with given id doesn't exists");
        }

        if (!_checkAvailabilityService.CheckTrackAvailability(user, track))
        {
            throw new UserAccessException("this user has no this track");
        }
        
        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name
        };
    }

    public async Task DeleteTrackAsync(string token, Guid trackId, CancellationToken cancellationToken)
    {
        if (trackId.Equals(Guid.Empty))
        {
            throw new InvalidArgumentsException("Guid can't be empty");   
        }
        
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);

        var track = await _context.Tracks.FirstOrDefaultAsync(
            item => item.Id.Equals(trackId), cancellationToken: cancellationToken);
        
        if (track is null)
        {
            throw new InvalidArgumentsException("track with given id doesn't exists");
        }
        
        if (!_checkAvailabilityService.CheckTrackAvailability(user, track))
        {
            throw new UserAccessException("this user has no this track");
        }

        _context.Tracks.Remove(track);
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}