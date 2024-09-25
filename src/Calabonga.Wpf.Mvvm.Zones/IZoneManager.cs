namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// ZoneManager interface
/// </summary>
public interface IZoneManager
{
    /// <summary>
    /// Executes Zone activation for generic type Content and ViewModel
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="zoneName"></param>
    void ActivateZone<TView, TViewModel>(string zoneName)
        where TView : IZoneView
        where TViewModel : IZoneViewModel;

    /// <summary>
    /// // Calabonga: Summary required (IZoneManager 2024-09-25 08:43)
    /// </summary>
    public event EventHandler<ZoneItem>? Activating;

    /// <summary>
    /// // Calabonga: Summary required (IZoneManager 2024-09-25 08:43)
    /// </summary>
    public event EventHandler<ZoneItem>? Activated;

    /// <summary>
    /// // Calabonga: Summary required (IZoneManager 2024-09-25 08:43)
    /// </summary>
    public event EventHandler<ZoneItem>? Deactivating;

    /// <summary>
    /// // Calabonga: Summary required (IZoneManager 2024-09-25 08:43)
    /// </summary>
    public event EventHandler<ZoneItem>? Deactivated;

    /// <summary>
    /// // Calabonga: Summary required (IZoneManager 2024-09-25 08:43)
    /// </summary>
    void Remove(IZoneViewModel viewModel);
}