using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class ImageService : IImageService
{
    public Track AddImageToTrack(Track track, Image image)
    {
        track.TrackImage = image;
        return track;
    }

    public Playlist AddImageToPlaylist(Playlist playlist, Image image)
    {
        playlist.PlaylistImage = image;
        return playlist;
    }
}