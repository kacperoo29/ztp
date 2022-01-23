using ElectricPlayer.API.Core;
using ElectricPlayer.API.Eventing;

namespace ElectricPlayer.API.Events;

public class SongChanged : Subject
{
    public Song Song { get; set; }
}