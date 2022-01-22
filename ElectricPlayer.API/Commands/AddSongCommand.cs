using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class AddSongCommand : ICommand
{
    private MusicPlayer _player;
    private Song _song;

    public AddSongCommand(MusicPlayer player, Song song)
    {
        _player = player;
        _song = song;
    }

    public void Execute()
    {
        _song = PlaylistIO.PopulateMetadata(_song);
        _player.Playlist.Songs.Add(_song);
        _player.CreateIterator();
        _player.State.Unlock();
    }
}