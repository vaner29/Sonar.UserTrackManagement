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
    public async Task<IActionResult> Create([FromHeader] Guid userId, [FromQuery] string name)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.Create(userId, name));
    }

    [HttpPost]
    public async Task<IActionResult> AddTrack([FromHeader] Guid userId, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.AddTrack(userId, playlistId, trackId);
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveTrack([FromHeader] Guid userId, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        if (playlistId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        await _service.RemoveTrack(userId, playlistId, trackId);
        return Ok();
    }
}