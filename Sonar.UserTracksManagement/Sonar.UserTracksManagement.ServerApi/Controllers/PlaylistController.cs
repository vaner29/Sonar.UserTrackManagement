using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;
using Sonar.UserTracksManagement.Core.Entities;

namespace ServerApi.Controllers;

[ApiController]
[Route("/playlist")]
public class PlaylistController : Controller
{
    private readonly IPlaylistApplicationService _service;

    public PlaylistController(IPlaylistApplicationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.CreateAsync(token, name, cancellationToken));
    }

    [HttpPost]
    [Route("track")]
    public async Task<ActionResult> AddTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        await _service.AddTrackAsync(token, playlistId, trackId, cancellationToken);
        return Ok();
    }

    [HttpDelete]
    [Route("track")]
    public async Task<ActionResult> RemoveTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        await _service.RemoveTrackAsync(token, playlistId, trackId, cancellationToken);
        return Ok();
    }

    [HttpGet]
    [Route("all-tracks")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracksOfPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetTracksFromPlaylistAsync(token, playlistId, cancellationToken));
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<Playlist>>> GetAllPlaylist(
        [FromHeader(Name = "Token")] string token,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetUserPlaylistsAsync(token, cancellationToken));
    }

    [HttpGet]
    public async Task<ActionResult<Playlist>> GetPlaylist(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid playlistId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetUserPlaylistAsync(token, playlistId, cancellationToken));
    }

    [HttpGet]
    [Route("with-tag")]
    public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylistWithTag(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] string tag,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetUserPlaylistsWithTagAsync(token, tag, cancellationToken));
    }
}