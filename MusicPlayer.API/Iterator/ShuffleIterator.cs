using System;
using System.Collections;
using MusicPlayer.API.Core;

namespace MusicPlayer.API.Iterator
{
    public class ShuffleIterator : IIterator
    {
        private List<int> _indices;
        private int _idx = 0;
        public List<Song> Songs { get; private set; }

        public ShuffleIterator(ICollection<Song> songs)
        {
            Songs = songs.ToList();
            _indices = Enumerable.Range(0, Songs.Count).ToList();

            var rnd = new Random();
            for (int i = Songs.Count - 1; i > 1; --i)
            {
                int j = rnd.Next(0, Songs.Count);
                int tmp = _indices[i];
                _indices[i] = _indices[j];
                _indices[j] = tmp;
            }

        }

        public Song GetNext()
        {
            if (_idx == Songs.Count - 1)
                _idx = -1;

            return Songs[_indices[++_idx]];
        }

        public Song GetCurrent()
        {
            return Songs[_indices[_idx]];
        }

        public Song GetPrevious()
        {
            if (_idx == 0)
                _idx = Songs.Count;

            return Songs[_indices[--_idx]];
        }
    }
}