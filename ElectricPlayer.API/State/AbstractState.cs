using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.State
{
    public abstract class AbstractState
    {
        protected MusicPlayer _player;

        public AbstractState(MusicPlayer player)
        {
            _player = player;
        }

        public abstract void Play(Song song);
        public abstract void NextSong();
        public abstract void PreviousSong();
        public abstract void Unlock();
        public abstract void Seek(long time);
    }
}