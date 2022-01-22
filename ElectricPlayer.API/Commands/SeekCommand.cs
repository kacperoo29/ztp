using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class SeekCommand : ICommand
{
    private MusicPlayer _target;
    private long _timestamp;
    
    public SeekCommand(MusicPlayer target, long timestamp)
    {
        _target = target;
        _timestamp = timestamp;
    }


    public void Execute()
    {
        _target.State.Seek(_timestamp);
    }
}