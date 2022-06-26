using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;

namespace ServerApi.Controllers;

[ApiController]
[Route("/image")]
public class ImageController : Controller
{
    private readonly IImageApplicationService _service;

    public ImageController(IImageApplicationService service)
    {
        _service = service;
    }
    
    [HttpPost("upload")]
    public async Task<ActionResult<Guid>> UploadImage(
        [FromHeader(Name="Token")] string token,
        [FromHeader(Name="Name")] string name,
        [FromForm] ImageFormDto form)
    {
        var stream = form.Content.OpenReadStream();
        return Ok(await _service.SaveImage(token, name, stream));
    }
}