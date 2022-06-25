using System.ComponentModel.DataAnnotations;

namespace Sonar.UserTracksManagement.Core.Entities;

public class Tag
{
    public Tag(string name)
    {
        Name = name;
    }
    [Key]
    public string Name { get; private init; }
}