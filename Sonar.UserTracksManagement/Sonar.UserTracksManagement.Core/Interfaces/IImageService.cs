using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface IImageService
{
    public Track AddImageToTrack(Track track, Image image);
    public Playlist AddImageToPlaylist(Playlist playlist, Image image);
}