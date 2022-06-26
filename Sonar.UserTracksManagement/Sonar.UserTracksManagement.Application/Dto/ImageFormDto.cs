using Microsoft.AspNetCore.Http;

namespace Sonar.UserTracksManagement.Application.Dto;

public class ImageFormDto
{
    public IFormFile Content;
    public string name; 
}