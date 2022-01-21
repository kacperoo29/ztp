using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using MusicPlayer.API.Core;
using MusicPlayer.API.IO;

namespace MusicPlayer.API
{
    public class XMLPlaylist : Playlist
    {
        public override List<Song> Songs { get; protected set; }

        public XMLPlaylist(string path)
        {
            var io = new XMLPlaylistIO();
            Songs = io.Import(path);
        }
    }
}