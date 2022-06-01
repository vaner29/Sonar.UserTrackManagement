using Microsoft.EntityFrameworkCore;
using Sonar.UserProfile.ApiClient;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class UserTracksApplicationService : IUserTracksApplicationService
{
    private readonly UserTracksManagementDatabaseContext _context;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserTrackService _userTracksService;
    private ICheckAvailabilityService _checkAvailabilityService;
    public UserTracksApplicationService(
        IAuthorizationService trackManagerService, 
        UserTracksManagementDatabaseContext context, 
        IUserTrackService userTracksService,
        ICheckAvailabilityService checkAvailabilityService)
    {
        _authorizationService = trackManagerService;
        _context = context;
        _userTracksService = userTracksService;
        _checkAvailabilityService = checkAvailabilityService;
    }

    public async Task<Guid> AddTrackAsync(string token, string name)
    {
        var user = await _authorizationService.GetUserAsync(token);
        var track = _userTracksService.AddNewTrack(user.Id, name);
        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();
        return track.Id;
    }

    public async Task<bool> CheckAccessToTrackAsync(string token, Guid trackId)
    {
        var user = await _authorizationService.GetUserAsync(token);
        return _context.Tracks.FirstOrDefault(item => item.OwnerId == user.Id) is not null;
    }

    public async Task<IEnumerable<TrackDto>> GetAllUserTracksAsync(string token)
    {
        var user = await _authorizationService.GetUserAsync(token);
        var tracks = _context.Tracks.Where(item => item.OwnerId == user.Id).ToList();
        return tracks.Select(item => new TrackDto()
        {
            Id = item.Id,
            Name = item.Name
        });
    }

    public async Task<TrackDto> GetTrackAsync(string token, Guid trackId)
    {
        var user = await _authorizationService.GetUserAsync(token);
        var track = await _context.Tracks.FirstOrDefaultAsync(item => item.OwnerId == user.Id);
        if (track is null)
        {
            throw new InvalidArgumentsException("");
        }

        return new TrackDto
        {
            Id = track.Id,
            Name = track.Name
        };
    }

    public async Task DeleteTrackAsync(string token, Guid trackId)
    {
        var user = await _authorizationService.GetUserAsync(token);
        var track = await _context.Tracks.FirstOrDefaultAsync(item => item.OwnerId == user.Id);
        if (track is null)
        {
            throw new InvalidArgumentsException();
        }

        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync();
    }
}