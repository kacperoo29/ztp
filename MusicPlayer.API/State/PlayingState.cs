using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class PlayingState : State
    {
        public PlayingState(MusicPlayer player)
            : base(player)
        {

        }

        public override void NextSong()
        {
            var song = _player.Iterator?.GetNext();
            if (song != null) 
            {
                _player.Play(song);
            }
        }

        public override void Play(Song song)
        {
            _player.PausePlayback();
            _player.ChangeState(new ReadyState(_player));
        }

        public override void PreviousSong()
        {
            var song = _player.Iterator?.GetPrevious();
            if (song != null) 
            {
                _player.Play(song);
            }
        }

        public override void Seek(long time)
        {
            _player.SeekImpl(time);
        }

        public override void Unlock()
        {

        }
    }
}