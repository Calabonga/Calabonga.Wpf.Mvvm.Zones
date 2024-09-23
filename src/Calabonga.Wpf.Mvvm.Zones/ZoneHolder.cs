using System.Windows;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Storage as singleton
/// </summary>
public class ZoneHolder
{
    private static readonly Lazy<ZoneHolder> Lazy = new(new ZoneHolder());
    public static ZoneHolder Instance => Lazy.Value;

    private readonly ICollection<IZone> _zones = new List<IZone>();

    /// <summary>
    /// Register a zone from attached property (XAML)
    /// </summary>
    /// <param name="element"></param>
    /// <param name="name"></param>
    public void RegisterZone(DependencyObject element, string name)
    {
        _zones.Add(new Zone(name, element));
    }

    /// <summary>
    /// Returns a zone if it registered
    /// </summary>
    /// <param name="zoneName"></param>
    /// <returns></returns>
    public IZone? GetZone(string zoneName)
    {
        return _zones.FirstOrDefault(x => x.Name == zoneName);
    }
}