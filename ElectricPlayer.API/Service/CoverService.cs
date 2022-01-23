using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Service;

public class CoverService : ICoverService
{
    public byte[] GetCover(Song song)
    {
        var file = TagLib.File.Create(song.Path);

        return file.Tag.Pictures[0].Data.Data;
    }
}