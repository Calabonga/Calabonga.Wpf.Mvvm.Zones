using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Manager
/// </summary>
public class ZoneManager : IZoneManager
{
    private readonly IServiceProvider _serviceProvider;

    public ZoneManager(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    #region Events

    public event EventHandler<ZoneView>? Activating;

    public event EventHandler<ZoneView>? Activated;

    public event EventHandler<ZoneView>? Deactivating;

    public event EventHandler<ZoneView>? Deactivated;

    #endregion

    /// <summary>
    /// Executes Zone activation for generic type View and ViewModel
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="zoneName"></param>
    public void ActivateZone<TView, TViewModel>(string zoneName)
        where TView : IZoneView
        where TViewModel : IZoneViewModel
    {
        var zone = ZoneHolder.Instance.GetZone(zoneName);
        if (zone is null)
        {
            throw new ArgumentNullException(nameof(zone));
        }

        var activeZone = zone.GetActive();

        if (activeZone is not null && zoneName == zone.Name)
        {
            if (typeof(TView) == activeZone.Type)
            {
                return;
            }

            OnDeactivating(activeZone);
            activeZone.DeactivateView();
            OnDeactivated(activeZone);
        }

        using var scope = _serviceProvider.CreateScope();
        var view = scope.ServiceProvider.GetRequiredService<TView>();
        var viewModel = scope.ServiceProvider.GetRequiredService<TViewModel>();
        view.DataContext = viewModel;
        var zoneView = zone.CreateOrActivate(view, OnActivating);
        OnActivated(zoneView);
    }

    protected virtual void OnActivating(ZoneView e) => Activating?.Invoke(this, e);

    protected virtual void OnActivated(ZoneView e) => Activated?.Invoke(this, e);

    protected virtual void OnDeactivating(ZoneView e) => Deactivating?.Invoke(this, e);

    protected virtual void OnDeactivated(ZoneView e) => Deactivated?.Invoke(this, e);
}
