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
    public async Task<ActionResult<Guid>> Create([FromHeader] string token, [FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.CreateAsync(token, name));
    }

    [HttpPost]
    [Route("/track")]
    public async Task<ActionResult> AddTrack([FromHeader] string token, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.AddTrackAsync(token, playlistId, trackId);
        return Ok();
    }
    
    [HttpDelete]
    [Route("/track")]
    public async Task<ActionResult> RemoveTrack([FromHeader] string token, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.RemoveTrackAsync(token, playlistId, trackId);
        return Ok();
    }
}