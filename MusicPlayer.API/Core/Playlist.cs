using MusicPlayer.API.Iterator;

namespace MusicPlayer.API.Core
{
    public abstract class Playlist : IPlaylist
    {
        public abstract ICollection<Song> Songs { get; protected set; }

        public IIterator CreateIterator(IteratorType type)
        {
            switch(type)
            {
                case IteratorType.Ordered:
                    return new NormalIterator(Songs);
                case IteratorType.Shuffle:
                    return new ShuffleIterator(Songs);
            }

            return null;
        }
    }
}