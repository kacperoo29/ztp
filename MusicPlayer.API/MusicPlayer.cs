using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class MusicPlayer
    {
        public State State { get; private set; }
        public IPlaylist? Playlist { get; private set; }

        public MusicPlayer()
        {
            State = new ReadyState(this);
        }

        public void ChangeState(State state)
        {
            State = state;
        }

        public void Play()
        {
            State.Play();
        }

        public void NextSong()
        {
            State.NextSong();
        }

        public void PreviousSong()
        {
            State.PreviousSong();
        }

        public void LoadPlaylist(IPlaylist playlist)
        {
            Playlist = playlist;
        }

        public void StartPlayback()
        {

        }

        public void StopPlayback()
        {

        }
    }
}