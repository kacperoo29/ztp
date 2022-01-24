using ElectricPlayer.API.Core;

namespace ElectricPlayer.API.Service;

public class CachedCoverService : ICoverService
{
    private static CachedCoverService? _instance;
    public static CachedCoverService Instance
    {
        get { return _instance ??= new CachedCoverService(); }
    }

    private readonly ICoverService _coverService;
    private readonly Dictionary<string, byte[]> _coverCache;

    protected CachedCoverService()
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