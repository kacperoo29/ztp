using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ElectricPlayer.API;
using ElectricPlayer.API.Commands;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.Eventing;
using ElectricPlayer.API.Events;
using ElectricPlayer.API.Playlist;
using ElectricPlayer.API.State;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace ElectricPlayer.Player.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IObserver
    {
        public MusicPlayer MusicPlayer { get; private set; }

        public ReactiveCommand<Unit, Unit> OnClickCommand { get; }
        public ReactiveCommand<Unit, Unit> OnNextSong { get; }
        public ReactiveCommand<Unit, Unit> OnSaveToJson { get; }

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
        
        private StatusBarViewModel _trackStatus;
        
        public StatusBarViewModel TrackStatus
        {
            get => _trackStatus;
            set => this.RaiseAndSetIfChanged(ref _trackStatus, value);
        }

        public MainWindowViewModel()
        {
            MusicPlayer = new API.MusicPlayer();
            // TODO: Select file from disc, decide if xml or json
            MusicPlayer.ExecuteCommand(
                new LoadPlaylistCommand(
                    MusicPlayer,
                    new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"))
                );

            TrackStatus = new StatusBarViewModel(MusicPlayer);

            MusicPlayer.ChangeState(new ReadyState(MusicPlayer));
            MusicPlayer.PlaybackStateChanged.Attach(this);
            RefreshData();

            OnClickCommand = ReactiveCommand.Create(() =>
            {
                MusicPlayer.ExecuteCommand(new PlayCommand(MusicPlayer, null));
            });

            OnNextSong = ReactiveCommand.Create(() =>
            {
                MusicPlayer.ExecuteCommand(new NextCommand(MusicPlayer));
                RefreshData();
            });

            OnSaveToJson = ReactiveCommand.Create(() =>
            {
                MusicPlayer.ExecuteCommand(
                    new SavePlaylistCommand(MusicPlayer,
                    PlaylistFormat.JSON,
                    "/home/kacper/repos/ztp/new.json")
                );
            });
        }

        public void AddToPlaylist(string[] results)
        {
            foreach (var result in results)
                MusicPlayer.ExecuteCommand(new AddSongCommand(MusicPlayer, new Song(result)));

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

        }
    }
}