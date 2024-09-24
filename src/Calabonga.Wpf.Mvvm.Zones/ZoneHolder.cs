using System.Windows;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Storage as singleton
/// </summary>
public sealed class ZoneHolder
{
    private readonly IList<IZone> _zones = new List<IZone>();

    /// <summary>
    /// Register a zone from attached property (XAML)
    /// </summary>
    /// <param name="element"></param>
    /// <param name="name"></param>
    public void RegisterZone(DependencyObject element, string name) => _zones.Add(new Zone(name, element));

    /// <summary>
    /// Returns a zone if it registered
    /// </summary>
    /// <param name="zoneName"></param>
    /// <returns></returns>
    public IZone? GetZone(string zoneName) => _zones.FirstOrDefault(x => x.Name == zoneName);

    #region singleton

    private static readonly Lazy<ZoneHolder> Lazy = new Lazy<ZoneHolder>(() => new ZoneHolder());

    private ZoneHolder() { }

    /// <summary>
    /// Instance of the <see cref="ZoneHolder"/>
    /// </summary>
    public static ZoneHolder Instance => Lazy.Value;

    #endregion
}