using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class LoadPlaylistCommand : ICommand
{
    private MusicPlayer _player;
    private IPlaylist _playlist;

    public LoadPlaylistCommand(MusicPlayer player, IPlaylist playlist)
    {
        _player = player;
        _playlist = playlist;
    }

    public void Execute()
    {
        _player.Playlist = _playlist;
        _player.CreateIterator();
        _player.State.Unlock();
    }
}