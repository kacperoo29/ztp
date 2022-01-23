using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class ToggleShuffleCommand : ICommand
{
    public ToggleShuffleCommand()
    {
        
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
        {
            var idx = player.Iterator.Idx;
            player._currentIterator = player._currentIterator == IteratorType.Shuffle
                ? IteratorType.Ordered
                : IteratorType.Shuffle;
            player.CreateIterator();
            player.Iterator.SetCurrent(idx);
        }
    }
}