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
            var song = _player.Iterator?.GetNext();
            _player.SongChanged.Song = song;
            _player.SongChanged.Notify();
        }
        
        public override void Play(Song? song)
        {
            _player.ResumePlayback();
            _player.ChangeState(new PlayingState(_player));
        }

        public override void PreviousSong()
        {
            var song = _player.Iterator?.GetPrevious();
            _player.SongChanged.Song = song;
            _player.SongChanged.Notify();
        }

        public override void Seek(long time)
        {
            throw new NotImplementedException();
        }

        public override void Unlock()
        {
            
        }
    }
}