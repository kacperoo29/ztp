using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ElectricPlayer.Player.Views;

public class StatusBar : UserControl
{
    public StatusBar()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}