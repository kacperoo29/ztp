using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class SavePlaylistCommand : ICommand
{
    private PlaylistFormat _format;
    private string _path;

    public SavePlaylistCommand(PlaylistFormat format, string path)
    {
        _format = format;
        _path = path;
    }
    
    public void Execute(object sender)
    {
        PlaylistIO io = _format switch
        {
            PlaylistFormat.JSON => new JSONPlaylistIO(),
            PlaylistFormat.XML => new XMLPlaylistIO(),
            _ => throw new InvalidEnumArgumentException()
        };

        if (sender is MusicPlayer player)
            io.Export(player.Playlist, _path);
    }
}