using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class PlaylistService : IPlaylistService
{
    public PlaylistService()
    {
    }


    public Playlist CreateNewPlaylist(Guid userId, string name)
    {
         return new Playlist(userId, name);
    }

    public bool CheckPlaylistForTrack(Playlist playlist, Track track)
    {
        return playlist.Tracks.Any(item => item.Track.Id.Equals(track.Id));
    }

    public void AddTrackToPlaylist(Playlist playlist, Track track)
    {
        playlist.AddTrack(new PlaylistTrack((uint)playlist.Tracks.Count(), track));
    }
    
    public void RemoveTrackFromPlaylist(Playlist playlist, Track track)
    {
        playlist.RemoveTrack(playlist.Tracks.First(item => item.Track.Id.Equals(track.Id)));
    }
}