using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> AddTrack([FromHeader] Guid userId, [FromQuery] string name)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        return Ok(await _service.AddTrack(userId, name));
    }


    [HttpGet]
    public async Task<IActionResult> GetAllTracks([FromHeader] Guid userId)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.GetAllTracks(userId));
    }
    
    [HttpGet]
    [Route("isEnoughAccess")]
    public async Task<IActionResult> GetAllTracks([FromHeader] Guid userId, [FromQuery] Guid trackId)
    {
        if (userId.Equals(Guid.Empty)) return BadRequest();
        if (trackId.Equals(Guid.Empty)) return BadRequest();
        return Ok(await _service.CheckAccessToTrack(userId, trackId));
    }

}