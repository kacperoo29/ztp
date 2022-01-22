using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class PreviousCommand : ICommand
{
    private MusicPlayer _player;

    public PreviousCommand(MusicPlayer target)
    {
        _player = target;
    }
    
    public void Execute()
    {
        _player.State.PreviousSong();
    }
}