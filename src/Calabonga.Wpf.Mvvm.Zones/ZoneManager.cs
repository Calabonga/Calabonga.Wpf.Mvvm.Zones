using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Manager
/// </summary>
public class ZoneManager : IZoneManager
{
    private readonly IServiceProvider _serviceProvider;

    public ZoneManager(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public void ActivateZone<TZoneView, TZoneViewModel>(string zoneName)
        where TZoneView : IZoneView
        where TZoneViewModel : IZoneViewModel
    {
        var zone = ZoneHolder.Instance.GetZone(zoneName);
        if (zone is null)
        {
            throw new ArgumentNullException(nameof(zone));
        }

        var activeZone = zone.GetActive();

        if (activeZone is not null && zoneName == zone.Name)
        {
            if (typeof(TZoneView) == activeZone.Type)
            {
                return;
            }

            activeZone.DeactivateView();

        }

        using var scope = _serviceProvider.CreateScope();
        var view = scope.ServiceProvider.GetRequiredService<TZoneView>();
        var viewModel = scope.ServiceProvider.GetRequiredService<TZoneViewModel>();
        view.DataContext = viewModel;
        zone.CreateOrActivate(view);
    }
}
