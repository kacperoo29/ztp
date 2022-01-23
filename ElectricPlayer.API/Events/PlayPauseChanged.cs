using ElectricPlayer.API.Eventing;

namespace ElectricPlayer.API.Events;

public class PlayPauseChanged : Subject
{
    public bool IsPlaying { get; set; }
}