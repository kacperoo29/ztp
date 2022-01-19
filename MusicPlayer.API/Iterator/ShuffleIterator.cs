using System.Collections;
using MusicPlayer.API.Core;

namespace MusicPlayer.API.Iterator
{
    public class ShuffleIterator : IIterator
    {
        public List<Song> Songs { get; private set; }

        public ShuffleIterator(ICollection<Song> songs)
        {
            Songs = songs.ToList();
        }

        public IEnumerator<Song> GetEnumerator()
        {
            var rnd = new Random();

            while (true)
                yield return Songs[rnd.Next(0, Songs.Count)];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}