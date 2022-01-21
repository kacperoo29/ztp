using MusicPlayer.API;

var musicPlayer = new MusicPlayer.API.MusicPlayer();
musicPlayer.LoadPlaylist(new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"));
musicPlayer.MusicPlaybackEvent += delegate(object? sender, EventArgs eventArgs)
{
    var args = eventArgs as MediaPlaybackEventArgs;
    if (args != null)
    Console.WriteLine($"Length {args.Length}, time {args.Time}");
};

musicPlayer.Play(musicPlayer.Playlist?.Songs[0] ?? throw new InvalidDataException());

Console.ReadKey();
