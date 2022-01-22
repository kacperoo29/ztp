using LibVLCSharp.Shared;
using MusicPlayer.API.Core;
using MusicPlayer.API.IO;

namespace MusicPlayer.API
{
    public class MusicPlayer : IDisposable
    {
        // TODO: Replace C# events with Observer pattern?
        public event EventHandler? MusicPlaybackEvent;

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

        // TODO: Command pattern?
        public void Play(Song song)
        {
            State.Play(song);
        }

        public void Stop()
        {

        }

        public void Pause()
        {

        }

        public void Seek(long time)
        {
            State.Seek(time);
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

        public void SavePlaylistToJson(string path)
        {
            if (Playlist != null)
            {
                var io = new JSONPlaylistIO();
                io.Export(Playlist, path);
            }
        }

        public void SavePlaylistToXML(string path)
        {
            if (Playlist != null)
            {
                var io = new XMLPlaylistIO();
                io.Export(Playlist, path);
            }
        }

        // TODO: Move internal functions to another class and make MusicPlayer more facade-like
        internal void StartPlayback(Song? song)
        {
            if (song == null)
            {
                if (Iterator == null)
                    throw new InvalidOperationException();

                song = Iterator.GetCurrent();
            }

            using var media = new Media(_libVLC, song.Path ?? throw new InvalidDataException());
            _mediaPlayer = new MediaPlayer(media);
            _mediaPlayer.Play();

            _mediaPlayer.TimeChanged += delegate
            {
                MusicPlaybackEvent?.Invoke(this, new MediaPlaybackEventArgs()
                {
                    Time = _mediaPlayer.Time,
                    Length = _mediaPlayer.Length
                });
            };
        }

        internal void StopPlayback()
        {
            _mediaPlayer?.Stop();
        }

        internal void PausePlayback()
        {
            _mediaPlayer?.Pause();
        }

        internal void SeekImpl(long time)
        {
            if (_mediaPlayer != null)
                _mediaPlayer.Time = time;
        }

        public void Dispose()
        {
            _mediaPlayer?.Dispose();
            _libVLC.Dispose();
        }
    }
}