using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class AddSongCommand : ICommand
{
    private Song _song;

    public AddSongCommand(Song song)
    {
        _song = song;
    }

    public void Execute(object sender)
    {
        if (sender is MusicPlayer player)
        {
            _song = PlaylistIO.PopulateMetadata(_song);
            player.Playlist.Songs.Add(_song);
            player.CreateIterator();
            player.State.Unlock();
            
            player.PlaylistChanged.Notify();
        }
    }
}