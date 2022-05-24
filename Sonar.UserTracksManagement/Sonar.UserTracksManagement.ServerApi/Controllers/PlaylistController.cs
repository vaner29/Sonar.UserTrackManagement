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
    public async Task<ActionResult<Guid>> Create([FromHeader(Name = "Token")] string token, [FromQuery] string name)
    {
        return Ok(await _service.CreateAsync(token, name));
    }

    [HttpPost]
    [Route("/track")]
    public async Task<ActionResult> AddTrack([FromHeader(Name = "Token")] string token, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        await _service.AddTrackAsync(token, playlistId, trackId);
        return Ok();
    }
    
    [HttpDelete]
    [Route("/track")]
    public async Task<ActionResult> RemoveTrack([FromHeader(Name = "Token")] string token, [FromQuery] Guid playlistId, [FromQuery] Guid trackId)
    {
        await _service.RemoveTrackAsync(token, playlistId, trackId);
        return Ok();
    }
}