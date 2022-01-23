using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class SeekCommand : ICommand
{
    private long _timestamp;

    public SeekCommand(long timestamp)
    {
        _timestamp = timestamp;
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
            player.State.Seek(_timestamp);
    }
}