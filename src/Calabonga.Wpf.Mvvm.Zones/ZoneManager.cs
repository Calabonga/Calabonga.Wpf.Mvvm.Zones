namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Manager
/// </summary>
public sealed class ZoneManager : IZoneManager
{
    private readonly IMvvmObjectFactory _mvvmFactory;

    public ZoneManager(IMvvmObjectFactory mvvmFactory) => _mvvmFactory = mvvmFactory;

    #region Events

    /// <summary>
    /// Handler for Activating event
    /// </summary>
    public event EventHandler<ZoneItem>? Activating;

    /// <summary>
    /// Handler for Activated event
    /// </summary>
    public event EventHandler<ZoneItem>? Activated;

    /// <summary>
    /// Handler for Deactivating event
    /// </summary>
    public event EventHandler<ZoneItem>? Deactivating;

    /// <summary>
    /// Handler for Deactivated event
    /// </summary>
    public event EventHandler<ZoneItem>? Deactivated;

    /// <summary>
    /// Removes <see cref="ZoneItem"/> from zone
    /// </summary>
    /// <param name="viewModel"></param>
    public void Remove(IZoneViewModel viewModel)
        => ZoneHolder.Instance.RemoveFromZones(viewModel, OnDeactivating, OnDeactivated);

    #endregion

    /// <summary>
    /// Executes Zone activation for generic type Content and ViewModel
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="zoneName"></param>
    public void ActivateZone<TView, TViewModel>(string zoneName)
        where TView : IZoneView
        where TViewModel : IZoneViewModel
    {
        var zone = ZoneHolder.Instance.GetZone(zoneName);
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

        var view = _mvvmFactory.Create<TView, TViewModel>(onViewModelCreated: viewModel =>
        {
            ZoneHolder.Instance.RemoveFromZones(viewModel, OnDeactivating, OnDeactivated);
            if (viewModel is ZoneViewModelBase zoneViewModel)
            {
                zoneViewModel.ZoneManager = this;
            }
        });

        var zoneView = zone.CreateOrActivate(view, OnActivating);
        OnActivated(zoneView);
    }

    private void OnActivating(ZoneItem e)
    {
        if (((IZoneView)e.Content).DataContext is IZoneViewModel viewModel)
        {
            viewModel.OnActivate();
        }
        Activating?.Invoke(this, e);
    }

    private void OnActivated(ZoneItem e) => Activated?.Invoke(this, e);

    private void OnDeactivating(ZoneItem e)
    {
        if (((IZoneView)e.Content).DataContext is IZoneViewModel viewModel)
        {
            viewModel.OnDeactivate();
        }
        Deactivating?.Invoke(this, e);
    }

    private void OnDeactivated(ZoneItem e) => Deactivated?.Invoke(this, e);
}
