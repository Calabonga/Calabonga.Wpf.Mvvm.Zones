namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone for content
/// </summary>
public interface IZone
{
    string Name { get; }

    ZoneView CreateOrActivate<TZoneView>(TZoneView view, Action<ZoneView> onActivating) where TZoneView : IZoneView;

    ZoneView? GetActive();
}