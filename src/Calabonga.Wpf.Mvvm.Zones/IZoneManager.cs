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

    // Calabonga: Remove before pull request (IZoneManager 2024-09-24 11:07)

    public event EventHandler<ZoneItem>? Activating;

    public event EventHandler<ZoneItem>? Activated;

    public event EventHandler<ZoneItem>? Deactivating;

    public event EventHandler<ZoneItem>? Deactivated;

    void Remove(IZoneViewModel viewModel);
}