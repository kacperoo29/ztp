using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using MusicPlayer.API.Core;
using MusicPlayer.API.IO;

namespace MusicPlayer.API
{
    public class JSONPlaylist : Playlist
    {
        public override List<Song> Songs { get; protected set; }

        public JSONPlaylist(string path)
        {
            var io = new JSONPlaylistIO();
            Songs = io.Import(path);
        }    
    }
}