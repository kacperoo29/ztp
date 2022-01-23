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
            var song = _player.Iterator.GetNext();

            _player.StartPlayback(song);
            _player.SongChanged.Song = song;
            _player.SongChanged.Notify();
        }

        public override void Play(Song? song)
        {
            _player.PausePlayback();
            _player.ChangeState(new PausedState(_player));

            _player.PlayPauseChanged.IsPlaying = false;
            _player.PlayPauseChanged.Notify();
        }

        public override void PreviousSong()
        {
            var song = _player.Iterator.GetPrevious();

            _player.StartPlayback(song);
            _player.SongChanged.Song = song;
            _player.SongChanged.Notify();
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