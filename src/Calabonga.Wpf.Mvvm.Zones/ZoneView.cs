using System.Windows;

namespace Calabonga.Wpf.Mvvm.Zones;

public class ZoneView
{
    public ZoneView(Type type, FrameworkElement view)
    {
        Type = type;
        View = view;
    }

    public Type Type { get; }

    public object View { get; }

    public bool IsActive { get; private set; }

    public void DeactivateView() => IsActive = false;

    public void ActivateView() => IsActive = true;

    public object? DataContext { get; set; }
}
