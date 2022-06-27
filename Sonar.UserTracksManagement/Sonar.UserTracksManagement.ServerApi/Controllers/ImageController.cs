using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;

namespace ServerApi.Controllers;

[ApiController]
[Route("/image")]
public class ImageController : Controller
{
    private readonly IImageApplicationService _service;

    public ImageController(IImageApplicationService service)
    {
        _service = service;
    }
    
    [HttpPost("upload")]
    public async Task<ActionResult<Guid>> UploadImage(
        [FromHeader(Name="Token")] string token,
        [FromHeader(Name="Path")] string path,
        IFormFile form,
        CancellationToken cancellationToken)
    {
        var stream = form.OpenReadStream();
        return Ok(await _service.SaveImageAsync(token, path, stream, cancellationToken));
    }

    [HttpPost("image-to-track")]
    public async Task<ActionResult> SetImageToTrack(
        [FromHeader(Name="Token")] string token,
        [FromQuery] Guid trackId,
        [FromQuery] Guid imageId,
        CancellationToken cancellationToken)
    {
        await _service.SetImageToTrackAsync(token, trackId, imageId, cancellationToken);
        return Ok();
    }

    [HttpPost("image-to-playlist")]
    public async Task<ActionResult> SetImageToPlaylist(
        [FromHeader(Name="Token")] string token,
        [FromQuery] Guid playlistId,
        [FromQuery] Guid imageId,
        CancellationToken cancellationToken)
    {
        await _service.SetImageToPlaylistAsync(token, playlistId, imageId, cancellationToken);
        return Ok();
    }

    [HttpGet("image-by-track")]
    public async Task<ActionResult<byte[]>> GetImageByTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetImageContentByTrackAsync(token, trackId, cancellationToken));
    }
    
    [HttpGet("image-by-playlist")]
    public async Task<ActionResult<byte[]>> GetImageByPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetImageContentByPlaylistAsync(token, playlistId, cancellationToken));
    }
}