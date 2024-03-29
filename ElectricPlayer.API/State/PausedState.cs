using ElectricPlayer.API.Core;
using LibVLCSharp.Shared;

namespace ElectricPlayer.API.State
{
    public class PausedState : AbstractState
    {
        public PausedState(MusicPlayer player)
            : base(player)
        {
            
        }

        public override void NextSong()
        {
            var song = _player.Iterator.GetNext();
            _player.SongChanged.Song = song;
            _player.SongChanged.Notify();
        }
        
        public override void Play(Song? song)
        {
            _player.ResumePlayback();
            _player.ChangeState(new PlayingState(_player));
            
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
            var song = _player.Iterator.GetPrevious();
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