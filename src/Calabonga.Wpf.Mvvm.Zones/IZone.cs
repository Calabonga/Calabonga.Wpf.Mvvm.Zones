using System.Windows;

namespace Calabonga.Wpf.Mvvm.Zones;

public interface IZone
{
    string Name { get; }

    void SetContent(FrameworkElement element);
}