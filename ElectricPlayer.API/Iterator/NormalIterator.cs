using System.Collections;
using System.Collections.Generic;
using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Iterator
{
    public class NormalIterator : IIterator
    {
        public List<Song> Songs { get; private set; }

        public int Idx
        {
            get => _idx;
        }

        private int _idx = 0;

        public NormalIterator(List<Song> songs)
        {
            Songs = songs;
        }

        public Song GetNext()
        {
            if (_idx == Songs.Count - 1)
                _idx = -1;

            return Songs[++_idx];
        }

        public Song GetCurrent()
        {
            return Songs[_idx];
        }

        public Song GetPrevious()
        {
            if (_idx == 0)
                _idx = Songs.Count;

            return Songs[--_idx];
        }

        public void SetCurrent(int index)
        {
            _idx = index;
        }
    }
}