using System.Collections;
using System.Collections.Generic;
using MusicPlayer.API.Core;

namespace MusicPlayer.API.Iterator
{
    public class NormalIterator : IIterator
    {
        public ICollection<Song> Songs { get; private set; }
        private int _idx = 0;

        public NormalIterator(ICollection<Song> songs)
        {
            Songs = songs;
        }

        public IEnumerator<Song> GetEnumerator()
        {
            foreach (var song in Songs)
                yield return song;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
    }
}