using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;

namespace ServerApi.Controllers;

[ApiController]
[Route("/tracks")]
public class UserTracksController : Controller
{
    private readonly IUserTracksApplicationService _service;

    public UserTracksController(IUserTracksApplicationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddTrack([FromHeader] Guid userId, [FromQuery] string name)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.AddTrackAsync(userId, name));
    }


    [HttpGet]
    [Route("/all")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracks([FromHeader] Guid userId)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        return Ok(await _service.GetAllTracksAsync(userId));
    }
    
    [HttpGet]
    public async Task<ActionResult<TrackDto>> GetTrack([FromHeader] Guid userId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.GetTrackAsync(userId, trackId));
    }
    
    [HttpGet]
    [Route("/isEnoughAccess")]
    public async Task<ActionResult<bool>> CheckAccessToTrack([FromHeader] Guid userId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return Unauthorized();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.CheckAccessToTrackAsync(userId, trackId));
    }

}