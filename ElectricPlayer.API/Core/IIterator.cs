using System.Collections.Generic;

namespace ElectricPlayer.API.Core
{
    public interface IIterator
    {
        public int Idx { get; }
        Song GetNext();
        Song GetCurrent();
        Song GetPrevious();
        void SetCurrent(int index);
    }
}