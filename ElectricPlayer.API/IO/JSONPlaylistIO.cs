using System.Text.Json;
using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.IO
{
    public class JSONPlaylistIO : PlaylistIO
    {
        protected override List<Song> Deserialize(string data)
        {
            return JsonSerializer.Deserialize<List<Song>>(data) ?? throw new InvalidDataException();
        }

        protected override string Serialize(List<Song> songs)
        {
            return JsonSerializer.Serialize(songs);
        }

    }
}