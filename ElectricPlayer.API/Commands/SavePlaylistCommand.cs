using System.ComponentModel;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.IO;

namespace ElectricPlayer.API.Commands;

public class SavePlaylistCommand : ICommand
{
    private MusicPlayer _player;
    private PlaylistFormat _format;
    private string _path;

    public SavePlaylistCommand(MusicPlayer target, PlaylistFormat format, string path)
    {
        _player = target;
        _format = format;
        _path = path;
    }
    
    public void Execute()
    {
        PlaylistIO io = _format switch
        {
            PlaylistFormat.JSON => new JSONPlaylistIO(),
            PlaylistFormat.XML => new XMLPlaylistIO(),
            _ => throw new InvalidEnumArgumentException()
        };

        io.Export(_player.Playlist, _path);
    }
}