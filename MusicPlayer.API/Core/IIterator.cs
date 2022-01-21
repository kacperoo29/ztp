using System.Collections.Generic;

namespace MusicPlayer.API.Core
{
    public interface IIterator
    {
        Song GetNext();
        Song GetCurrent();
        Song GetPrevious();
    }
}