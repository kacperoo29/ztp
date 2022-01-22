using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Playlist
{
    public class JSONPlaylist : Core.Playlist
    {
        public override List<Song> Songs { get; protected set; }

        public JSONPlaylist(string path)
        {
            var io = new JSONPlaylistIO();
            Songs = io.Import(path);
        }    
    }
}