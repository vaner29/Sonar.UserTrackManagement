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

    public PlaylistTrack AddTrackToPlaylist(Playlist playlist, Track track)
    {
        var playlistTrack = new PlaylistTrack((uint)playlist.Tracks.Count(), track);
        playlist.AddTrack(playlistTrack);
        return playlistTrack;
    }
    
    public PlaylistTrack RemoveTrackFromPlaylist(Playlist playlist, Track track)
    {
        var playlistTrack = playlist.Tracks.First(item => item.Track.Id.Equals(track.Id));
        playlist.RemoveTrack(playlistTrack);
        return playlistTrack;
    }

    public List<Track> GetTracksFromPlaylist(Playlist playlist)
    {
        return playlist.Tracks.Select(playlistTrack => playlistTrack.Track).ToList();
    }
}