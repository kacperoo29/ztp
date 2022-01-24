using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.Events;
using ElectricPlayer.API.IO;
using ElectricPlayer.API.Service;
using ElectricPlayer.API.State;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace ElectricPlayer.API
{
    public class MusicPlayer : IDisposable
    {
        public PlaybackStateChanged PlaybackStateChanged { get; private set; }
        public SongChanged SongChanged { get; private set; }
        public PlayPauseChanged PlayPauseChanged { get; private set; }
        public PlaylistChanged PlaylistChanged { get; private set; }

        public AbstractState State { get; private set; }
        public IPlaylist Playlist { get; internal set; }
        public IIterator Iterator { get; private set; }

        internal IteratorType _currentIterator;
        private readonly LibVLC _libVLC = new LibVLC();
        private MediaPlayer? _mediaPlayer;
        private readonly Stack<ICommand> _commandHistory;
        
        public MusicPlayer()
        {
            State = new LockedState(this);
            _currentIterator = IteratorType.Ordered;
            Playlist = new Core.Playlist();
            Iterator = Playlist.CreateIterator(_currentIterator);
            PlaybackStateChanged = new();
            SongChanged = new();
            PlayPauseChanged = new();
            PlaylistChanged = new();
            _commandHistory = new();

            LibVLCSharp.Shared.Core.Initialize();
        }

        public void ChangeState(AbstractState state)
        {
            State = state;
        }
        
        public void ExecuteCommand(ICommand command)
        {
            command.Execute(this);
            // TODO: Optional undo last command
            _commandHistory.Push(command);
        }

        public byte[] GetCover(Song song)
        {
            return CachedCoverService.Instance.GetCover(song);
        }
 
        internal void CreateIterator()
        {
            Iterator = Playlist.CreateIterator(_currentIterator);
        }

        // TODO: Move internal functions to another class and make MusicPlayer more facade-like
        internal Song StartPlayback(Song? song)
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
            
            _mediaPlayer.EndReached += delegate
            {
                ChangeState(new ReadyState(this));
            };

            return song;
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