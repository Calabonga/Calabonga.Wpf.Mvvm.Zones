using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Zone Manager
/// </summary>
public class ZoneManager : IZoneManager
{
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<Type, object> _views = new();

    public ZoneManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ActivateZone<TZoneView, TZoneViewModel>(string zoneName)
        where TZoneView : IZoneView
        where TZoneViewModel : IZoneViewModel
    {
        var zone = ZoneHolder.Instance.GetZone(zoneName);
        if (zone is null)
        {
            throw new ArgumentNullException(nameof(zone));
        }

        //if (_views.ContainsKey(typeof(TZoneView)))
        //{
        //    var viewFromCache = _views[typeof(TZoneView)];
        //    zone.SetContent((FrameworkElement)viewFromCache);
        //    return;
        //}

        using var scope = _serviceProvider.CreateScope();
        var view = scope.ServiceProvider.GetRequiredService<TZoneView>();
        var viewModel = scope.ServiceProvider.GetRequiredService<TZoneViewModel>();
        view.DataContext = viewModel;

        if (view is not FrameworkElement useControl)
        {
            throw new InvalidOperationException($"Unable activate map point {zoneName}");
        }

        // _views.Add(typeof(TZoneView), view);

        zone.SetContent(useControl);
    }


}
