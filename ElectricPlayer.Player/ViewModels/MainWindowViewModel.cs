using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using ElectricPlayer.API.Service;
using ElectricPlayer.API.State;
using LibVLCSharp.Shared;
using ReactiveUI;

namespace ElectricPlayer.Player.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IObserver
    {
        public MusicPlayer MusicPlayer { get; private set; }

        private Bitmap? _cover;
        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }

        private IEnumerable<string> _songs;
        public IEnumerable<string> Songs
        {
            get => _songs;
            private set => this.RaiseAndSetIfChanged(ref _songs, value);
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

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public MainWindowViewModel()
        {
            MusicPlayer = new API.MusicPlayer();

            TrackStatus = new StatusBarViewModel(MusicPlayer);
            ControlPanel = new ControlPanelViewModel(MusicPlayer);

            MusicPlayer.SongChanged.Attach(this);
            MusicPlayer.PlaylistChanged.Attach(this);

            this.WhenAnyValue(x => x.SelectedIndex)
                .Subscribe(x =>
                {
                    if (x >= 0 && MusicPlayer.Playlist.Songs.Count > x)
                        MusicPlayer.ExecuteCommand(new PlayCommand(MusicPlayer.Playlist.Songs[x]));
                });
        }

        private void RefreshData()
        {
            var current = MusicPlayer.Iterator?.GetCurrent();
            if (current != null)
            {
                var coverData = MusicPlayer.GetCover(current);
                if (coverData != null && coverData.Length > 0)
                    Cover = new Bitmap(new MemoryStream(coverData));
                Title = current.Metadata.Title;
            }
        }

        public void Update(Subject subject)
        {
            switch (subject)
            {
                case SongChanged e:
                    RefreshData();
                    break;
                case PlaylistChanged e:
                    Songs = MusicPlayer.Playlist.Songs.Select(x => x.Metadata.Title);
                    break;
            }
        }
    }
}