using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Application.Repositories;
using Sonar.UserTracksManagement.Application.Tools;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Services;

public class ImageApplicationService : IImageApplicationService
{
    private readonly IImageRepository _imageRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly ITrackRepository _trackRepository;
    private readonly IPlaylistRepository _playlistRepository;
    
    public ImageApplicationService(
        IImageRepository imageRepository, 
        IAuthorizationService authorizationService,
        ITrackRepository trackRepository, 
        IPlaylistRepository playlistRepository)
    {
        _imageRepository = imageRepository;
        _authorizationService = authorizationService;
        _trackRepository = trackRepository;
        _playlistRepository = playlistRepository;
    }

    public async Task<Guid> SaveImageAsync(
        string token, 
        string path, 
        Stream fileStream, 
        CancellationToken cancellationToken)
    {
        var id = await _authorizationService.GetUserIdAsync(token, cancellationToken);
        var image = new Image(Path.Combine(id.ToString(), path), id);
        await _imageRepository.SaveImageAsync(image, fileStream, cancellationToken);
        return image.Id;
    }

    public async Task SetImageToTrackAsync(
        string token, 
        Guid trackId,
        Guid imageId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);
        var track = await _trackRepository.GetToOwnerAsync(user, trackId, cancellationToken);
        var image = await _imageRepository.GetImageAsync(imageId, cancellationToken);
        await _trackRepository.AddImageToTrackAsync(track, image, cancellationToken);
    }   

    public async Task SetImageToPlaylistAsync(
        string token, 
        Guid playlistId,
        Guid imageId,
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository.GetToOwnerAsync(user, playlistId, cancellationToken);
        var image = await _imageRepository.GetImageAsync(imageId, cancellationToken);
        await _playlistRepository.AddImageToPlaylistAsync(playlist, image, cancellationToken);
    }

    public async Task<byte[]> GetImageContentByTrackAsync(
        string token, 
        Guid trackId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);
        var track = await _trackRepository.GetToOwnerAsync(user, trackId, cancellationToken);
        return await _imageRepository
            .GetImageContentAsync(track.TrackImage, cancellationToken);;
    }

    public async Task<byte[]> GetImageContentByPlaylistAsync(
        string token, 
        Guid playlistId, 
        CancellationToken cancellationToken)
    {
        var user = await _authorizationService.GetUserAsync(token, cancellationToken);
        var playlist = await _playlistRepository.GetToOwnerAsync(user, playlistId, cancellationToken);
        return await _imageRepository
            .GetImageContentAsync(playlist.PlaylistImage, cancellationToken);;
    }
}
