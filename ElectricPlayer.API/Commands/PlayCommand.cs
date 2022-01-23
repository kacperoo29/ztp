using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Commands;

public class PlayCommand : ICommand
{
    private Song? _song;
    
    public PlayCommand(Song? song)
    {
        _song = song;
    }


    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
            player.State.Play(_song);
    }
}