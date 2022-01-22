namespace ElectricPlayer.API.Eventing;

public interface IObserver
{
    void Update(Subject subject);
}