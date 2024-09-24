namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone for content
/// </summary>
public interface IZone
{
    string Name { get; }

    IEnumerable<ZoneItem> Views { get; }

    ZoneItem CreateOrActivate<TZoneView>(TZoneView view, Action<ZoneItem> onActivating) where TZoneView : IZoneView;

    ZoneItem? GetActive();

    void RemoveItem(ZoneItem zoneItem);
}