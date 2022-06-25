using Microsoft.EntityFrameworkCore;
using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Database;

public class UserTracksManagementDatabaseContext : DbContext
{
    public UserTracksManagementDatabaseContext(DbContextOptions<UserTracksManagementDatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
        Tracks.Load();
        Playlists.Load();
        PlaylistTracks.Load();
        MetaDataInfos.Load();
        Tags.Load();
    }
    
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
    public DbSet<MetaDataInfo> MetaDataInfos { get; set; }
    public DbSet<Tag> Tags { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Playlist>().HasMany<PlaylistTrack>();
        modelBuilder.Entity<PlaylistTrack>().HasOne<Track>();
        modelBuilder.Entity<Playlist>().HasOne<MetaDataInfo>();
        modelBuilder.Entity<Track>().HasOne<MetaDataInfo>();
        modelBuilder.Entity<MetaDataInfo>().HasMany<Tag>();
    }
}