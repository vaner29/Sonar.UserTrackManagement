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
    public async Task<ActionResult<Guid>> AddTrack(
        [FromHeader(Name = "Token")] string token, 
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.AddTrackAsync(token, name, cancellationToken));
    }


    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<TrackDto>>> GetAllUserTracks(
        [FromHeader(Name = "Token")] string token,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAllUserTracksAsync(token, cancellationToken));
    }
    
    [HttpGet]
    public async Task<ActionResult<TrackDto>> GetTrack(
        [FromHeader(Name = "Token")] string token, 
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.GetTrackAsync(token, trackId, cancellationToken));
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteTrack(
        [FromHeader(Name = "Token")] string token, 
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        await _service.DeleteTrackAsync(token, trackId, cancellationToken);
        return Ok();
    }
    
    [HttpGet]
    [Route("is-enough-access")]
    public async Task<ActionResult<bool>> CheckAccessToTrack(
        [FromHeader(Name = "Token")] string token,
        [FromQuery] Guid trackId,
        CancellationToken cancellationToken)
    {
        return Ok(await _service.CheckAccessToTrackAsync(token, trackId, cancellationToken));
    }

}