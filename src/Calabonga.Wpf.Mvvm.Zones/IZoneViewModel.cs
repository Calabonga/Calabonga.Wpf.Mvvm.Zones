using CommunityToolkit.Mvvm.ComponentModel;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// // Calabonga: Summary required (IZoneViewModel 2024-09-24 12:07)
/// </summary>
public interface IZoneViewModel
{
    IZoneManager ZoneManager { get; }

    /// <summary>
    /// // Calabonga: Summary required (IZoneViewModel 2024-09-24 12:07)
    /// </summary>
    void OnDeactivate();

    /// <summary>
    /// // Calabonga: Summary required (IZoneViewModel 2024-09-24 12:08)
    /// </summary>
    void OnActivate();
}

public abstract class ZoneViewModelBase : ObservableObject, IZoneViewModel
{
    public IZoneManager ZoneManager { get; set; } = null!;

    public virtual void OnDeactivate() { }

    public virtual void OnActivate() { }
}