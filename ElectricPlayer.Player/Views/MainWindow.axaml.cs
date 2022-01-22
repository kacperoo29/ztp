using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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

        private async void Button_OnClick(object? sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() {Name = "Audio files", Extensions = {"mp3", "flac", "m4a"}});
            var result = await dialog.ShowAsync(this);
            
            if (result != null)
            {
                var context = this.DataContext as MainWindowViewModel;
                context?.AddToPlaylist(result);
            }
        }
    }
}