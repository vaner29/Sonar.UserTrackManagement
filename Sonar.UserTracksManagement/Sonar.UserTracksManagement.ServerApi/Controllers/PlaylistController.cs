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
    
    [HttpGet]
    [Route("/all-tracks")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracksOfPlaylist([FromHeader(Name = "Token")] string token, [FromQuery] Guid playlistId)
    {
        return Ok(await _service.GetTracksFromPlaylistAsync(token, playlistId));
    }
    [HttpGet]
    [Route("/all")]
    public async Task<ActionResult<IEnumerable<Playlist>>> GetAllPlaylist([FromHeader(Name = "Token")] string token)
    {
        return Ok(await _service.GetUserPlaylistsAsync(token));
    }
}