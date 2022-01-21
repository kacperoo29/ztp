using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class XMLPlaylist : Playlist
    {
        public override List<Song> Songs { get; protected set; }

        public XMLPlaylist(string path)
        {
            var serializer = new XmlSerializer(typeof(List<Song>));

            using var reader = new FileStream(path, FileMode.Open);
            Songs = (List<Song>)(serializer.Deserialize(reader) ?? throw new InvalidDataException());
        }
    }
}