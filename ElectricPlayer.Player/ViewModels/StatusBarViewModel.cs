using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ElectricPlayer.API;
using ElectricPlayer.API.Commands;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.Eventing;
using ElectricPlayer.API.Events;
using ReactiveUI;

namespace ElectricPlayer.Player.ViewModels;

public class StatusBarViewModel : ViewModelBase, IObserver
{
    private readonly MusicPlayer _musicPlayer;
    private bool _isTrackSeeking;
    private double _position;
    private double _seekPosition;
    private string _status;
    private string _currentTime;
    private string _currentDuration;
    private TimeSpan _duration;
    
    public double Position
    {
        get => _position;
        set => this.RaiseAndSetIfChanged(ref _position, value);
    }

    public double SeekPosition
    {
        get => _seekPosition;
        set => this.RaiseAndSetIfChanged(ref _seekPosition, value);
    }

    public bool IsTrackSeeking
    {
        get => _isTrackSeeking;
        set => this.RaiseAndSetIfChanged(ref _isTrackSeeking, value);
    }
    
    public string Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }
    
    public string CurrentTime
    {
        get => _currentTime;
        private set => this.RaiseAndSetIfChanged(ref _currentTime, value);
    }

    public string CurrentDuration
    {
        get { return _currentDuration; }
        set { this.RaiseAndSetIfChanged(ref _currentDuration, value); }
    }
    
    public TimeSpan Duration
    {
        get => _duration;
        private set => this.RaiseAndSetIfChanged(ref _duration, value);
    }

    public StatusBarViewModel(MusicPlayer musicPlayer)
    {
        _musicPlayer = musicPlayer;
        _musicPlayer.PlaybackStateChanged.Attach(this);
        _musicPlayer.SongChanged.Attach(this);
        
        this.WhenAnyValue(x => x.SeekPosition)
            .Skip(1)
            .Subscribe(x =>
            {
                _musicPlayer.ExecuteCommand(
                    new SeekCommand((long)(SeekPosition * Duration.TotalMilliseconds)));
            });

        this.WhenAnyValue(x => x.Status)
            .Throttle(TimeSpan.FromSeconds(2))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(_ => { Status = ""; });
    }

    private string FormatTimeSpan(TimeSpan x)
    {
        return $"{x.Minutes:00}:{x.Seconds:00}";
    }

    public void Update(Subject subject)
    {
        switch (subject)
        {
            case PlaybackStateChanged e:
                Duration = TimeSpan.FromMilliseconds(e.Length);
                CurrentDuration = FormatTimeSpan(Duration);
                CurrentTime = FormatTimeSpan(TimeSpan.FromMilliseconds(e.Time));
                break;
            case SongChanged e:
                Position = SeekPosition = 0;
                break;
        }
    }
}