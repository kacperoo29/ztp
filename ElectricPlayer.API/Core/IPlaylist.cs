using System.Collections;

namespace ElectricPlayer.API.Core
{
    public interface IPlaylist
    {
        List<Song> Songs { get; }
        IIterator CreateIterator(IteratorType type);
        void SaveToFile(string path);
    }
}