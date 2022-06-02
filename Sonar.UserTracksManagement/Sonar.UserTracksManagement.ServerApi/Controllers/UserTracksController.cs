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
    public async Task<ActionResult<Guid>> AddTrack([FromHeader(Name = "Token")] string token, [FromQuery] string name)
    {
        return Ok(await _service.AddTrackAsync(token, name));
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllUserTracks([FromHeader(Name = "Token")] string token)
    {
        return Ok(await _service.GetAllUserTracksAsync(token));
    }
    
    [HttpGet]
    public async Task<ActionResult<TrackDto>> GetTrack([FromHeader(Name = "Token")] string token, [FromQuery] Guid trackId)
    {
        return Ok(await _service.GetTrackAsync(token, trackId));
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteTrack([FromHeader(Name = "Token")] string token, [FromQuery] Guid trackId)
    {
        await _service.DeleteTrackAsync(token, trackId);
        return Ok();
    }
    
    [HttpGet]
    [Route("is-enough-access")]
    public async Task<ActionResult<bool>> CheckAccessToTrack([FromHeader(Name = "Token")] string token, [FromQuery] Guid trackId)
    {
        return Ok(await _service.CheckAccessToTrackAsync(token, trackId));
    }

}