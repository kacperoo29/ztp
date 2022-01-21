using LibVLCSharp.Shared;
using MusicPlayer.API.Core;

namespace MusicPlayer.API
{
    public class MusicPlayer : IDisposable
    {
        public State State { get; private set; }
        public IPlaylist? Playlist { get; private set; }
        public IIterator? Iterator { get; private set; }

        private IteratorType _currentIterator;
        private LibVLC _libVLC = new LibVLC();
        private MediaPlayer? _mediaPlayer;

        public MusicPlayer()
        {
            State = new LockedState(this);
            _currentIterator = IteratorType.Ordered;

            LibVLCSharp.Shared.Core.Initialize();
        }

        public void ChangeState(State state)
        {
            State = state;
        }

        public void Play(Song song)
        {
            State.Play(song);
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

        public void StartPlayback(Song song)
        {
            using var media = new Media(_libVLC, song.Path ?? throw new InvalidDataException());
            _mediaPlayer = new MediaPlayer(media);
            _mediaPlayer.Play();
        }

        public void StopPlayback()
        {
            _mediaPlayer?.Stop();
        }

        public void Dispose()
        {
            _mediaPlayer?.Dispose();
            _libVLC.Dispose();
        }
    }
}