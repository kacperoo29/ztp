using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public abstract class State
    {
        protected MusicPlayer _player;

        public State(MusicPlayer player)
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