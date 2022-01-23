using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ElectricPlayer.Player.Views;

public class ControlPanel : UserControl
{
    public ControlPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}