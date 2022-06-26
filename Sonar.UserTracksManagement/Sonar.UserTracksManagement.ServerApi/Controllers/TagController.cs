using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Core.Entities;

namespace ServerApi.Controllers;

[ApiController]
[Route("/tag")]
public class TagController : Controller
{
    private readonly ITagApplicationService _service;

    public TagController(ITagApplicationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Register(
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        await _service.RegisterTagAsync(name, cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Route("playlist-all")]
    public async Task<ActionResult<Tag>> GetFromPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetPlaylistTags(token, playlistId, cancellationToken));
    }

    [HttpGet]
    [Route("track-all")]
    public async Task<ActionResult<Tag>> GetFromTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetTrackTags(token, trackId, cancellationToken));
    }

    [HttpPost]
    [Route("assign-to-playlist")]
    public async Task<ActionResult> AssignToPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        [FromQuery] string tagName,
        CancellationToken cancellationToken)
    {
        await _service.AssignTagToPlaylistAsync(token, tagName, playlistId, cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("assign-to-track")]
    public async Task<ActionResult> AssignToTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid trackId,
        [FromQuery] string tagName,
        CancellationToken cancellationToken)
    {
        await _service.AssignTagToTrackAsync(token, tagName, trackId, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("remove-from-playlist")]
    public async Task<ActionResult> RemoveFromPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        [FromQuery] string tagName,
        CancellationToken cancellationToken)
    {
        await _service.RemoveTagFromPlaylistAsync(token, tagName, playlistId, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("remove-from-track")]
    public async Task<ActionResult> RemoveFromTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid trackId,
        [FromQuery] string tagName,
        CancellationToken cancellationToken)
    {
        await _service.RemoveTagFromPlaylistAsync(token, tagName, trackId, cancellationToken);
        return Ok();
    }
}