using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class PlayCommand : ICommand
{
    private MusicPlayer _target;
    private Song? _song;
    
    public PlayCommand(MusicPlayer target, Song? song)
    {
        _target = target;
        _song = song;
    }


    public void Execute()
    {
        _target.State.Play(_song);
    }
}