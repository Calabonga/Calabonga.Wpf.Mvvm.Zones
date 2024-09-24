namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// ZoneManager interface
/// </summary>
public interface IZoneManager
{
    /// <summary>
    /// Executes Zone activation for generic type View and ViewModel
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="zoneName"></param>
    void ActivateZone<TView, TViewModel>(string zoneName)
        where TView : IZoneView
        where TViewModel : IZoneViewModel;

    public event EventHandler<ZoneView>? Activating;

    public event EventHandler<ZoneView>? Activated;

    public event EventHandler<ZoneView>? Deactivating;

    public event EventHandler<ZoneView>? Deactivated;
}