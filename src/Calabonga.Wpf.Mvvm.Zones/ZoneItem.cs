using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Represents a <see cref="IZoneView"/> with the attached element from XAML.
/// </summary>
public class ZoneItem
{
    public ZoneItem(Type type, FrameworkElement content)
    {
        Type = type;
        Content = content;
    }

    /// <summary>
    /// Type of the zon item
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Object mapped to XAML as content <see cref="ContentControl"/>
    /// </summary>
    public object Content { get; }

    /// <summary>
    /// Indicates current zone item is active (visible)
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Deactivates current zone item
    /// </summary>
    public void DeactivateView() => IsActive = false;

    /// <summary>
    /// Activates current zone item
    /// </summary>
    public void ActivateView() => IsActive = true;
}
