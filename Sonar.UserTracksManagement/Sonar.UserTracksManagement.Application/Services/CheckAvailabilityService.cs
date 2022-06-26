﻿using Sonar.UserProfile.ApiClient.Interfaces;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class CheckAvailabilityService : ICheckAvailabilityService
{
    private IRelationshipApiClient _apiClient;
    public CheckAvailabilityService(IRelationshipApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<bool> CheckTrackAvailability(string token, User user, Track track, CancellationToken cancellationToken)
    {
        return track.TrackMetaDataInfo.AccessType switch
        {
            AccessType.Public => true,
            AccessType.Private => IsTrackOwner(user, track),
            AccessType.OnlyFans => await _apiClient.IsFriends(token, user.UserId, cancellationToken),
            _ => throw new NotImplementedException(
                $"Access type {Enum.GetName(track.TrackMetaDataInfo.AccessType)} not implemented yet")
        };
    }

    public bool CheckPlaylistAvailability(User user, Playlist playlist)
    {
        return IsPlaylistOwner(user, playlist);
    }

    public bool IsTrackOwner(User user, Track track)
    {
        return user.UserId == track.OwnerId;
    }

    public bool IsPlaylistOwner(User user, Playlist playlist)
    {
        return playlist.UserId == user.UserId;
    }
}