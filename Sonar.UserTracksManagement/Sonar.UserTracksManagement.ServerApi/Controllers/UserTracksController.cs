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
    public async Task<ActionResult<Guid>> AddTrack([FromHeader] string token, [FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.AddTrackAsync(token, name));
    }


    [HttpGet]
    [Route("/all")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllTracks([FromHeader] string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        return Ok(await _service.GetAllTracksAsync(token));
    }
    
    [HttpGet]
    public async Task<ActionResult<TrackDto>> GetTrack([FromHeader] string token, [FromQuery] Guid trackId)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.GetTrackAsync(token, trackId));
    }
    
    [HttpGet]
    [Route("/isEnoughAccess")]
    public async Task<ActionResult<bool>> CheckAccessToTrack([FromHeader] string token, [FromQuery] Guid trackId)
    {
        if (string.IsNullOrWhiteSpace(token)) return Unauthorized();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.CheckAccessToTrackAsync(token, trackId));
    }

}