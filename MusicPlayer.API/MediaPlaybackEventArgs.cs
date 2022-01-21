namespace MusicPlayer.API
{
    public class MediaPlaybackEventArgs : EventArgs
    {
       public long Time { get; set; }
       public long Length { get; set; }
    }
}