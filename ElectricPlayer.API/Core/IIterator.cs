using System.Collections.Generic;

namespace ElectricPlayer.API.Core
{
    public interface IIterator
    {
        Song GetNext();
        Song GetCurrent();
        Song GetPrevious();
    }
}