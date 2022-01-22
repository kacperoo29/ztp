using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ElectricPlayer.API.Playlist;
using LibVLCSharp.Shared;
using ElectricPlayer.API;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.Eventing;
using ElectricPlayer.API.Events;
using ElectricPlayer.API.State;
using ReactiveUI;

namespace ElectricPlayer.Player.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IObserver
    {
        // TODO: Change namespace
        public API.MusicPlayer MusicPlayer { get; private set; }

        public ReactiveCommand<Unit, Unit> OnClickCommand { get; }
        public ReactiveCommand<Unit, Unit> OnNextSong { get; }
        public ReactiveCommand<Unit, Unit> OnSaveToJson { get; }
        public ReactiveCommand<Unit, Unit> OnAddToPlaylist { get; }

        private Bitmap? _cover;

        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }

        private string? _title;

        public string? Title
        {
            get => _title;
            private set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public MainWindowViewModel()
        {
            MusicPlayer = new API.MusicPlayer();
            // TODO: Select file from disc, decide if xml or json
            MusicPlayer.LoadPlaylist(new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"));
            MusicPlayer.ChangeState(new ReadyState(MusicPlayer));
            MusicPlayer.PlaybackStateChanged.Attach(this);
            RefreshData();

            OnClickCommand = ReactiveCommand.Create(() => { MusicPlayer.Play(null); });

            OnNextSong = ReactiveCommand.Create(() =>
            {
                MusicPlayer.NextSong();
                RefreshData();
            });

            OnSaveToJson = ReactiveCommand.Create(() =>
            {
                MusicPlayer.SavePlaylistToJson("/home/kacper/repos/ztp/new.json");
            });
        }

        public void AddToPlaylist(string[] results)
        {
            foreach (var result in results)
                MusicPlayer.AddSong(new Song(result));
            
            RefreshData();
        }
                
        public async Task LoadCover(Stream stream)
        {
            await using (var imageStream = stream)
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }

        private void RefreshData()
        {
            var current = MusicPlayer.Iterator?.GetCurrent();
            if (current?.Metadata?.Artwork != null)
            {
                Cover = new Bitmap(new MemoryStream(current.Metadata.Artwork));
                Title = current.Metadata.Title;
            }
        }

        public void Update(Subject subject)
        {
            var sub = subject as PlaybackStateChanged;
            Console.WriteLine($"{sub?.Time}");
        }
    }
}