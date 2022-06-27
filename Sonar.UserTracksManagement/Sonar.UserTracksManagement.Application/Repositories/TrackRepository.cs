using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Application.Database;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Repositories;

public class TrackRepository : ITrackRepository
{
    private readonly IUserTrackService _trackService;
    private readonly ICheckAvailabilityService _availabilityService;
    private readonly UserTracksManagementDatabaseContext _databaseContext;

    public TrackRepository(
        IUserTrackService trackService,
        ICheckAvailabilityService availabilityService,
        UserTracksManagementDatabaseContext databaseContext)
    {
        _trackService = trackService;
        _availabilityService = availabilityService;
        _databaseContext = databaseContext;
    }

    public async Task<Track> AddAsync(
        Guid userId,
        string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidArgumentsException("Name can't be empty or contain only whitespaces");
        }

        var track = _trackService.AddNewTrack(userId, name);

        await _databaseContext.Tracks.AddAsync(track, cancellationToken);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return track;
    }

    public async Task<Track> GetToAvailableUserAsync(
        string token,
        User user,
        Guid trackId,
        CancellationToken cancellationToken)
    {
        var track = await GetAsync(trackId, cancellationToken);

        if (!await _availabilityService.CheckTrackAvailability(token, user, track, cancellationToken))
        {
            throw new UserAccessException("User doesn't have access to given track");
        }

        return track;
    }

    public async Task<Track> GetToOwnerAsync(
        User user,
        Guid trackId,
        CancellationToken cancellationToken)
    {
        var track = await GetAsync(trackId, cancellationToken);

        if (!_availabilityService.IsTrackOwner(user, track))
        {
            throw new UserAccessException("User isn't owner of given track");
        }

        return track;
    }

    public async Task DeleteAsync(
        User user, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var track = await GetToOwnerAsync(user, trackId, cancellationToken);
        _databaseContext.Tracks.Remove(track);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task ChangeTrackAccessLevelAsync(
        Track track, 
        AccessType newAccessType, 
        CancellationToken cancellationToken)
    {
        track.TrackMetaDataInfo.AccessType = newAccessType;
    }

    public Task<IEnumerable<Track>> GetUserAllAsync(
        string token, 
        User user, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _databaseContext.Tracks
                .AsEnumerable()
                .Where(item => _availabilityService
                    .CheckTrackAvailability(token, user, item, cancellationToken).Result));
    }

    public async Task<Track> GetAsync(
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        if (trackId.Equals(Guid.Empty))
        {
            throw new InvalidArgumentsException("Guid can't be empty");
        }

        var track = await _databaseContext.Tracks
            .FirstOrDefaultAsync(
                track => track.Id.Equals(trackId),
                cancellationToken: cancellationToken);

        if (track is null)
        {
            throw new NotFoundArgumentsException("Couldn't find track with given ID");
        }

        return track;
    }

    public Task<IEnumerable<Track>> GetTrackWithTagForAvailableUserAsync(
        string token,
        User user, 
        Tag tag, 
        CancellationToken cancellationToken)
    {
        return Task.FromResult(
            _databaseContext.Tracks
                .AsEnumerable()
                .Where(item => 
                    _availabilityService
                        .CheckTrackAvailability(token, user, item, cancellationToken).Result &&
                    item.TrackMetaDataInfo.Tags
                        .Contains(tag)));
    }

    public async Task AddImageToTrackAsync(Track track, Image image, CancellationToken cancellationToken)
    {
        track.TrackImage = image;
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}