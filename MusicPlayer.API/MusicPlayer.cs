using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class MusicPlayer
    {
        public State State { get; private set; }
        public IPlaylist? Playlist { get; private set; }
        public IIterator? Iterator { get; private set; }

        private IteratorType _currentIterator;

        public MusicPlayer()
        {
            State = new LockedState(this);
            _currentIterator = IteratorType.Ordered;
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
            Iterator = Playlist.CreateIterator(_currentIterator);

            State.Unlock();
        }

        public void ToggleShuffle()
        {
            _currentIterator = _currentIterator == IteratorType.Shuffle 
                ? IteratorType.Ordered 
                : IteratorType.Shuffle;
            Iterator = Playlist?.CreateIterator(_currentIterator);
        }

        public void SavePlaylist(string path)
        {
            if (Playlist != null)
                Playlist.SaveToFile(path);
        }

        public void StartPlayback()
        {

        }

        public void StopPlayback()
        {

        }
    }
}