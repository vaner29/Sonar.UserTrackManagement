﻿using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IPlaylistApplicationService
{
    Task<Guid> CreateAsync(string token, string name);
    Task AddTrackAsync(string token, Guid playlistId, Guid trackId);
    Task RemoveTrackAsync(string token, Guid playlistId, Guid trackId);
    Task<IEnumerable<TrackDto>> GetTracksFromPlaylistAsync(string token, Guid playlistId);
    Task<IEnumerable<Playlist>> GetUserPlaylistsAsync(string token);
    Task<Playlist> GetUserPlaylistAsync(string token, Guid playlistId);


}