using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class StopCommand : ICommand
{
    private MusicPlayer _player;

    public StopCommand(MusicPlayer target)
    {
        _player = target;
    }
    
    public void Execute()
    {
        
    }
}