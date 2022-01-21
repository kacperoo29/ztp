using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class LockedState : State
    {
        public LockedState(MusicPlayer player)
            : base(player)
        {
        }

        public override void NextSong()
        {
            
        }

        public override void Play(Song song)
        {
            
        }

        public override void PreviousSong()
        {
            
        }

        public override void Seek(long time)
        {
            
        }

        public override void Unlock()
        {
            _player.ChangeState(new ReadyState(_player));
        }
    }
}