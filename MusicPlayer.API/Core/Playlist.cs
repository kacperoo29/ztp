using MusicPlayer.API.Iterator;

namespace MusicPlayer.API.Core
{
    public abstract class Playlist : IPlaylist
    {
        public abstract List<Song> Songs { get; protected set; }

        public IIterator CreateIterator(IteratorType type) => type switch
        {
            IteratorType.Ordered => new NormalIterator(Songs),
            IteratorType.Shuffle => new ShuffleIterator(Songs),
            _ => throw new ArgumentOutOfRangeException(),
        };

        public void SaveToFile(string path)
        {

        }
    }
}