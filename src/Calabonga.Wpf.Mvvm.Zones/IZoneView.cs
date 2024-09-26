using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Default interface for ZoneItem
/// </summary>
public interface IZoneView
{
    /// <summary>
    /// Represents a DataContext for <see cref="FrameworkElement"/> For example, <see cref="UserControl"/>
    /// </summary>
    object? DataContext { get; set; }
}

