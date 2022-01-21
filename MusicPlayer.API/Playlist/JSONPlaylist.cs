using System.IO;
using System.Net.Sockets;
using System.Text.Json;
using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class JSONPlaylist : Playlist
    {
        public override List<Song> Songs { get; protected set; }

        public JSONPlaylist(string path)
        {
            string jsonString = File.ReadAllText(path);
            Songs = JsonSerializer.Deserialize<List<Song>>(jsonString) ?? throw new InvalidDataException();
        }    
    }
}