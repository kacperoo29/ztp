using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class PreviousCommand : ICommand
{
    public PreviousCommand()
    {
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
            player.State.PreviousSong();
    }
}