using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class NextCommand : ICommand
{
    public NextCommand()
    {
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
            player.State.NextSong();
    }
}