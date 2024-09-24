using System.Windows;

namespace Calabonga.Wpf.Mvvm.Zones;

public class ZoneItem
{
    public ZoneItem(Type type, FrameworkElement content)
    {
        Type = type;
        Content = content;
    }

    public Type Type { get; }

    public object Content { get; }

    public bool IsActive { get; private set; }

    public void DeactivateView() => IsActive = false;

    public void ActivateView() => IsActive = true;
}
