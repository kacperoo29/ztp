using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.State
{
    public class ReadyState : AbstractState
    {
        public ReadyState(MusicPlayer player)
            : base(player)
        {
        }

        public override void NextSong()
        {
            _player.Iterator.GetNext();
        }

        public override void Play(Song? song)
        {
            _player.SongChanged.Song = _player.StartPlayback(song);
            _player.ChangeState(new PlayingState(_player));
            _player.SongChanged.Notify();

            _player.PlayPauseChanged.IsPlaying = true;
            _player.PlayPauseChanged.Notify();

            if (song != null)
            {
                _player.Iterator.SetCurrent(_player.Playlist.Songs.IndexOf(song));
                _player.SongChanged.Song = song;
                _player.SongChanged.Notify();
            }
        }

        public override void PreviousSong()
        {
            _player.Iterator.GetPrevious();
        }

        public override void Seek(long time)
        {
            
        }

        public override void Unlock()
        {
        }
    }
}