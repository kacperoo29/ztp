using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Service;

public interface ICoverService
{
    byte[] GetCover(Song song);
}