using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Dto;

public class TrackDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid OwnerId { get; set; }
    public AccessType Type { get; set; }
}