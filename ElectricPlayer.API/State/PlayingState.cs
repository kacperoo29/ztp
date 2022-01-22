using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.State
{
    public class PlayingState : AbstractState
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
                _player.StartPlayback(song);
            }
        }

        public override void Play(Song song)
        {
            _player.PausePlayback();
            _player.ChangeState(new PausedState(_player));
        }

        public override void PreviousSong()
        {
            var song = _player.Iterator?.GetPrevious();
            if (song != null) 
            {
                _player.StartPlayback(song);
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