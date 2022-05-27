using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Database;

public class UserTracksManagementDatabaseContext : DbContext
{
    public UserTracksManagementDatabaseContext(DbContextOptions<UserTracksManagementDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        Users.Load();
        Tracks.Load();
        Playlists.Load();
        PlaylistTracks.Load();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasMany<Track>();
        modelBuilder.Entity<Playlist>().HasOne<User>();
        modelBuilder.Entity<Playlist>().HasMany<PlaylistTrack>();
        modelBuilder.Entity<PlaylistTrack>().HasOne<Track>();
    }
}