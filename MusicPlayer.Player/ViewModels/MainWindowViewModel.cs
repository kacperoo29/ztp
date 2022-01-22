using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using LibVLCSharp.Shared;
using MusicPlayer.API;
using ReactiveUI;

namespace MusicPlayer.Player.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // TODO: Change namespace
        public API.MusicPlayer MusicPlayer { get; private set; }
        private Bitmap? _cover;
        public ReactiveCommand<Unit, Unit> OnClickCommand { get; }

        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }

        public MainWindowViewModel()
        {
            MusicPlayer = new API.MusicPlayer();
            // TODO: Select file from disc, decide if xml or json
            MusicPlayer.LoadPlaylist(new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"));
            Cover = new Bitmap(new MemoryStream(MusicPlayer.Iterator.GetCurrent().Metadata.Artwork));
            OnClickCommand = ReactiveCommand.Create(() =>
            {
                MusicPlayer.Play(null);
            });
        }

        public async Task LoadCover(Stream stream)
        {
            await using (var imageStream = stream)
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }
    }
}
