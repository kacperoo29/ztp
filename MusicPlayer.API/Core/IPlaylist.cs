using System.Collections;

namespace MusicPlayer.API.Core
{
    public interface IPlaylist
    {
        ICollection<Song> Songs { get; }
        IIterator CreateIterator(IteratorType type);
    }
}