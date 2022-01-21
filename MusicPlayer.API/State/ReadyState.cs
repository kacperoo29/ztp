namespace MusicPlayer.API
{
    public class ReadyState : State
    {
        public ReadyState(MusicPlayer player)
            : base(player)
        {
            
        }

        public override void NextSong()
        {
            throw new NotImplementedException();
        }

        public override void Play()
        {
            _player.StartPlayback();
            _player.ChangeState(new PlayingState(_player));
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