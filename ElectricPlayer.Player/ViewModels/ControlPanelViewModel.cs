using System.Reactive;
using ElectricPlayer.API;
using ElectricPlayer.API.Commands;
using ElectricPlayer.API.Eventing;
using ElectricPlayer.API.Events;
using ReactiveUI;

namespace ElectricPlayer.Player.ViewModels;

public class ControlPanelViewModel : ViewModelBase, IObserver
{
    private readonly MusicPlayer _musicPlayer;

    public ReactiveCommand<Unit, Unit> PlayCommand { get; }
    public ReactiveCommand<Unit, Unit> BackCommand { get; }
    public ReactiveCommand<Unit, Unit> ForwardCommand { get; }

    private bool _isPlaying;

    public bool IsPlaying
    {
        get => _isPlaying;
        set => this.RaiseAndSetIfChanged(ref _isPlaying, value);
    }

    public ControlPanelViewModel(MusicPlayer musicPlayer)
    {
        _musicPlayer = musicPlayer;
        _musicPlayer.PlayPauseChanged.Attach(this);
        
        PlayCommand =
            ReactiveCommand.Create(() => { _musicPlayer.ExecuteCommand(new PlayCommand(_musicPlayer, null)); });

        BackCommand = ReactiveCommand.Create(() => { _musicPlayer.ExecuteCommand(new PreviousCommand(_musicPlayer)); });

        ForwardCommand = ReactiveCommand.Create(() => { _musicPlayer.ExecuteCommand(new NextCommand(_musicPlayer)); });
    }

    public void Update(Subject subject)
    {
        switch (subject)
        {
            case PlayPauseChanged e:
                IsPlaying = e.IsPlaying;
                break;
        }
    }
}