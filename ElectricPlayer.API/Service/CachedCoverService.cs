using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Service;

public class CachedCoverService : ICoverService
{
    private readonly ICoverService _coverService;
    private Dictionary<string, byte[]> _coverCache;

    public CachedCoverService()
    {
        _coverService = new CoverService();
        _coverCache = new Dictionary<string, byte[]>();
    }
    
    public byte[] GetCover(Song song)
    {
        if (!_coverCache.TryGetValue(song.Path, out var cover))
        {
            cover = _coverService.GetCover(song);
            _coverCache.Add(song.Path, cover);
        }

        return cover;
    }
}