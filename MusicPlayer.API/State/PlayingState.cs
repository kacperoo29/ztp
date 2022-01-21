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
            throw new NotImplementedException();
        }

        public override void Play(Song song)
        {
            _player.StopPlayback();
            _player.ChangeState(new ReadyState(_player));
        }

        public override void PreviousSong()
        {
            throw new NotImplementedException();
        }

        public override void Unlock()
        {
            
        }
    }
}