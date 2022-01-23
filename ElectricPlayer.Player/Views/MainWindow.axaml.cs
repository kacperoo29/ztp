using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DynamicData.Binding;
using ElectricPlayer.API.Commands;
using ElectricPlayer.API.Core;
using ElectricPlayer.API.Playlist;
using ElectricPlayer.Player.ViewModels;

namespace ElectricPlayer.Player.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void Import_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel context)
            {
                var dialog = new OpenFileDialog();
                dialog.Filters.Add(new FileDialogFilter() {Name = "Playlist formats", Extensions = {"json", "xml"}});
                dialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                dialog.AllowMultiple = false;

                var result = await dialog.ShowAsync(this);
                if (result == null)
                    return;

                if (result[0].EndsWith("json"))
                {
                    context.MusicPlayer.ExecuteCommand(new LoadPlaylistCommand(new JSONPlaylist(result[0])));
                }
                else if (result[0].EndsWith("xml"))
                {
                    context.MusicPlayer.ExecuteCommand(new LoadPlaylistCommand(new XMLPlaylist(result[0])));
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
        }

        private async void Export_OnClick(object? sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainWindowViewModel context)
            {
                var dialog = new SaveFileDialog();
                dialog.Filters.Add(new FileDialogFilter() {Name = "JSON", Extensions = {"json"}});
                dialog.Filters.Add(new FileDialogFilter() {Name = "XML", Extensions = {"xml"}});
                dialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                var result = await dialog.ShowAsync(this);

                if (result == null)
                    return;

                if (result.EndsWith("json"))
                {
                    context.MusicPlayer.ExecuteCommand(new SavePlaylistCommand(PlaylistFormat.JSON, result));
                }
                else if (result.EndsWith("xml"))
                {
                    context.MusicPlayer.ExecuteCommand(new SavePlaylistCommand(PlaylistFormat.XML, result));
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
        }

        private async void Add_OnClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.AllowMultiple = true;
            dialog.Filters.Add(new FileDialogFilter()
            {
                Name = "Audio formats", Extensions = {"aac", "flac", "m4a", "m4b", "mp3", "ogg", "opus", "wav", "wma"}
            });
            dialog.Directory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            var result = await dialog.ShowAsync(this);

            if (result != null && this.DataContext is MainWindowViewModel context)
            {
                foreach (var song in result)
                    context.MusicPlayer.ExecuteCommand(new AddSongCommand(new Song(song)));
            }
        }
    }
}