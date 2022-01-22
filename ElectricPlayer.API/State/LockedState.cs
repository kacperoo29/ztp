using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.State
{
    public class LockedState : AbstractState
    {
        public LockedState(MusicPlayer player)
            : base(player)
        {
        }

        public override void NextSong()
        {
            
        }

        public override void Play(Song? song)
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