using ElectricPlayer.API.Iterator;

namespace ElectricPlayer.API.Core
{
    public class Playlist : IPlaylist
    {
        public List<Song> Songs { get; protected set; }

        public IIterator CreateIterator(IteratorType type) => type switch
        {
            IteratorType.Ordered => new NormalIterator(Songs),
            IteratorType.Shuffle => new ShuffleIterator(Songs),
            _ => throw new ArgumentOutOfRangeException(),
        };

        public Playlist()
        {
            Songs = new List<Song>();
        }
    }
}