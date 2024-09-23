using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

public class Zone : IZone
{
    private readonly DependencyObject _control;

    public Zone(string mapName, DependencyObject element)
    {
        Name = mapName;
        _control = element;
    }

    public string Name { get; }

    public void SetContent(FrameworkElement value)
    {
        _control.SetValue(ContentControl.ContentProperty, value);
    }
}