using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class LoadPlaylistCommand : ICommand
{
    private IPlaylist _playlist;

    public LoadPlaylistCommand(IPlaylist playlist)
    {
        _playlist = playlist;
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
        {
            player.Playlist = _playlist;
            player.CreateIterator();
            player.State.Unlock();
            
            player.PlaylistChanged.Notify();
        }
    }
}