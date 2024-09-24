namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone for content
/// </summary>
public interface IZone
{
    string Name { get; }

    void CreateOrActivate<TZoneView>(TZoneView view) where TZoneView : IZoneView;

    ZoneView? GetActive();
}