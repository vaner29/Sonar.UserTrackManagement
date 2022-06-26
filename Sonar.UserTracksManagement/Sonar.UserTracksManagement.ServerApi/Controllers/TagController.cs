using Microsoft.AspNetCore.Mvc;
using Sonar.UserTracksManagement.Application.Interfaces;

namespace ServerApi.Controllers;

[ApiController]
[Route("/tag")]
public class TagController : Controller
{
    private readonly ITagApplicationService _service;

    public TagController(ITagApplicationService service)
    {
        _service = service;
    }
    
}