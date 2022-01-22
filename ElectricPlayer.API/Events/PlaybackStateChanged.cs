using ElectricPlayer.API.Eventing;

namespace ElectricPlayer.API.Events;

public class PlaybackStateChanged : Subject
{
    public long Time { get; set; }
    public long Length { get; set; }
}