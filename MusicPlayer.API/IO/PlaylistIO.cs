using LibVLCSharp.Shared;
using MusicPlayer.API.Core;

namespace MusicPlayer.API.IO
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

        private void PopulateMetadata(ref List<Song> songs)
        {
            foreach (var song in songs)
            {
                var media = new Media(new LibVLC(), song.Path ?? throw new InvalidDataException());
                var metadata = new Metadata();

                metadata.Title = media.Meta(MetadataType.Title);
                // TODO: Extract more metadata, plus artwork

                song.Metadata = metadata;
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