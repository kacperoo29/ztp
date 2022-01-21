using MusicPlayer.API.Core;

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
            _player.Iterator?.GetNext();
        }

        public override void Play(Song song)
        {
            _player.StartPlayback(song);
            _player.ChangeState(new PlayingState(_player));
        }

        public override void PreviousSong()
        {
            _player.Iterator?.GetPrevious();
        }

        public override void Seek(long time)
        {
            
        }

        public override void Unlock()
        {
            
        }
    }
}