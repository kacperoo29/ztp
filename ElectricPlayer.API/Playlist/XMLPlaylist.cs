using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Playlist
{
    public class XMLPlaylist : Core.Playlist
    {
        public XMLPlaylist(string path)
        {
            var io = new XMLPlaylistIO();
            Songs = io.Import(path);
        }
    }
}