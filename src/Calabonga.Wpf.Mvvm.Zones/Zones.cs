using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone registration helper for XAML
/// </summary>
public static class Zones
{
    #region AttechedProperty

    /// <summary>
    /// Zone Property
    /// </summary>
    public static readonly DependencyProperty ZoneProperty = DependencyProperty.RegisterAttached(
        "Zone",
        typeof(ZoneManager),
        typeof(ZoneManager),
        null);

    /// <summary>
    /// ZoneName Property
    /// </summary>
    public static readonly DependencyProperty ZoneNameProperty = DependencyProperty.RegisterAttached(
        "ZoneName",
        typeof(string),
        typeof(ZoneManager),
        new PropertyMetadata(defaultValue: null, propertyChangedCallback: OnSetMapNameCallback));

    public static void SetZoneName(ContentControl areaTarget, string placeName)
    {
        if (areaTarget == null)
        {
            throw new ArgumentNullException(nameof(areaTarget));
        }

        areaTarget.SetValue(ZoneNameProperty, placeName);
    }

    public static string? GetZoneName(DependencyObject areaTarget)
    {
        if (areaTarget == null)
        {
            throw new ArgumentNullException(nameof(areaTarget));
        }

        return areaTarget.GetValue(ZoneNameProperty) as string;
    }

    private static void OnSetMapNameCallback(DependencyObject element, DependencyPropertyChangedEventArgs args)
    {
        if (IsInDesignMode(element))
        {
            return;
        }

        var name = GetZoneName(element);
        ZoneHolder.Instance.RegisterZone(element, name!);
    }

    private static bool IsInDesignMode(DependencyObject element) => DesignerProperties.GetIsInDesignMode(element);

    #endregion
}