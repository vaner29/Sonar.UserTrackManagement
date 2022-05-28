﻿namespace Sonar.UserTracksManagement.Core.Entities;

public class Track
{
    private Track()
    {
    }

    public Track(Guid ownerId, string name)
    {
        Id = Guid.NewGuid();
        OwnerId = ownerId;
        Name = name;
    }

    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public Guid OwnerId { get; private init; }
}