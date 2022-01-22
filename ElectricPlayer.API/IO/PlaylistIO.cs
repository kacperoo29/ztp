using System.Linq;
using ElectricPlayer.API.Core;
using LibVLCSharp.Shared;

namespace ElectricPlayer.API.IO
{
    public abstract class PlaylistIO
    {
        public List<Song> Import(string path)
        {
            var data = ReadFile(path);

            var songs = Deserialize(data);
            PopulateMetadata(ref songs);

            return songs;
        }

        public void Export(IPlaylist playlist, string path)
        {
            var data = Serialize(playlist.Songs);
            WriteToFile(path, data);
        }

        public static Song PopulateMetadata(Song song)
        {
            var file = TagLib.File.Create(song.Path);

            song.Metadata  = new Metadata
            {
                // TODO: Pull other metadata
                Artwork = file.Tag.Pictures[0].Data.Data,
                Title = file.Tag.Title
            };

            return song;
        }

        private static void PopulateMetadata(ref List<Song> songs)
        {
            for (int i = 0; i < songs.Count; ++i)
            {
                songs[i] = PopulateMetadata(songs[i]);
            }
        }

        private void WriteToFile(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        private string ReadFile(string path)
        {
            return File.ReadAllText(path);
        }

        protected abstract string Serialize(List<Song> songs);
        protected abstract List<Song> Deserialize(string path);
    }
}