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
        public ReactiveCommand<Unit, Unit> OnSaveToJson { get; }

        private Bitmap? _cover;

        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }

        private StatusBarViewModel _trackStatus;

        public StatusBarViewModel TrackStatus
        {
            get => _trackStatus;
            set => this.RaiseAndSetIfChanged(ref _trackStatus, value);
        }

        private ControlPanelViewModel _controlPanel;

        public ControlPanelViewModel ControlPanel
        {
            get => _controlPanel;
            set => this.RaiseAndSetIfChanged(ref _controlPanel, value);
        }

        public MainWindowViewModel()
        {
            MusicPlayer = new API.MusicPlayer();
            // TODO: Select file from disc, decide if xml or json
            MusicPlayer.ExecuteCommand(
                new LoadPlaylistCommand(
                    new JSONPlaylist(@"/home/kacper/repos/ztp/test.json"))
            );

            TrackStatus = new StatusBarViewModel(MusicPlayer);
            ControlPanel = new ControlPanelViewModel(MusicPlayer);

            MusicPlayer.ChangeState(new ReadyState(MusicPlayer));
            MusicPlayer.SongChanged.Attach(this);

            OnSaveToJson = ReactiveCommand.Create(() =>
            {
                MusicPlayer.ExecuteCommand(
                    new SavePlaylistCommand(
                        PlaylistFormat.JSON,
                        "/home/kacper/repos/ztp/new.json")
                );
            });
        }

        private void RefreshData()
        {
            var current = MusicPlayer.Iterator?.GetCurrent();
            if (current?.Metadata?.Artwork != null)
            {
                Cover = new Bitmap(new MemoryStream(current.Metadata.Artwork));
            }
        }

        public void Update(Subject subject)
        {
            switch (subject)
            {
                case SongChanged e:
                    RefreshData();
                    break;
            }
        }
    }
}