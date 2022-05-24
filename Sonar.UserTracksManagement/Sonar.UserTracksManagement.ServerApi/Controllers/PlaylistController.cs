using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Interfaces;

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
    public async Task<ActionResult<Guid>> Create([FromHeader] Guid userId, [FromQuery] string name)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.CreateAsync(userId, name));
    }

    [HttpPost]
    [Route("/track")]
    public async Task<ActionResult> AddTrack([FromHeader] Guid userId, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.AddTrackAsync(userId, playlistId, trackId);
        return Ok();
    }
    
    [HttpDelete]
    [Route("/track")]
    public async Task<ActionResult> RemoveTrack([FromHeader] Guid userId, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.RemoveTrackAsync(userId, playlistId, trackId);
        return Ok();
    }
}