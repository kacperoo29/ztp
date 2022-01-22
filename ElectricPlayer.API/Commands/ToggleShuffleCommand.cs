using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class ToggleShuffleCommand : ICommand
{
    private MusicPlayer _player;

    public ToggleShuffleCommand(MusicPlayer player)
    {
        _player = player;
    }

    public void Execute()
    {
        _player._currentIterator = _player._currentIterator == IteratorType.Shuffle
                ? IteratorType.Ordered
                : IteratorType.Shuffle;
        _player.CreateIterator();
    }
}