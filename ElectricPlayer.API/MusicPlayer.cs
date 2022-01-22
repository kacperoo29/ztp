using ElectricPlayer.API.Core;
using ElectricPlayer.API.Events;
using ElectricPlayer.API.IO;
using ElectricPlayer.API.State;
using LibVLCSharp.Shared;

namespace ElectricPlayer.API
{
    public class MusicPlayer : IDisposable
    {
        public PlaybackStateChanged PlaybackStateChanged { get; private set; }
        public AbstractState State { get; private set; }
        public IPlaylist Playlist { get; private set; }
        public IIterator Iterator { get; private set; }

        private IteratorType _currentIterator;
        private LibVLC _libVLC = new LibVLC();
        private MediaPlayer? _mediaPlayer;

        public MusicPlayer()
        {
            State = new LockedState(this);
            _currentIterator = IteratorType.Ordered;
            Playlist = new Core.Playlist();
            Iterator = Playlist.CreateIterator(_currentIterator);
            PlaybackStateChanged = new();

            LibVLCSharp.Shared.Core.Initialize();
        }

        public void ChangeState(AbstractState state)
        {
            State = state;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }

        public void LoadPlaylist(IPlaylist playlist)
        {
            Playlist = playlist;
            Iterator = Playlist.CreateIterator(_currentIterator);

            State.Unlock();
        }

        public void AddSong(Song song)
        {
            song = PlaylistIO.PopulateMetadata(song);
            Playlist.Songs.Add(song);
            Iterator = Playlist.CreateIterator(_currentIterator);
        }

        public void ToggleShuffle()
        {
            _currentIterator = _currentIterator == IteratorType.Shuffle
                ? IteratorType.Ordered
                : IteratorType.Shuffle;
            Iterator = Playlist.CreateIterator(_currentIterator);
        }

        public void SavePlaylistToJson(string path)
        {
            var io = new JSONPlaylistIO();
            io.Export(Playlist, path);
        }

        public void SavePlaylistToXML(string path)
        {
            var io = new XMLPlaylistIO();
            io.Export(Playlist, path);
        }

        // TODO: Move internal functions to another class and make MusicPlayer more facade-like
        internal void StartPlayback(Song? song)
        {
            _mediaPlayer?.Dispose();

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
                PlaybackStateChanged.Length = _mediaPlayer.Length;
                PlaybackStateChanged.Time = _mediaPlayer.Time;
                PlaybackStateChanged.Notify();
            };
        }

        internal void ResumePlayback()
        {
            _mediaPlayer?.Play();
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