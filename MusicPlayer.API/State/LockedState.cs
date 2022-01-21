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

        public override void Play()
        {
            
        }

        public override void PreviousSong()
        {
            
        }

        public override void Unlock()
        {
            _player.ChangeState(new ReadyState(_player));
        }
    }
}