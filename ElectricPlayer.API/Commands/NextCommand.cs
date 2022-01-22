using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class NextCommand : ICommand
{
    private MusicPlayer _player;

    public NextCommand(MusicPlayer target)
    {
        _player = target;
    }
    
    public void Execute()
    {
        _player.State.NextSong();
    }
}