using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Factory for View and ViewModel generation from Dependency Container.
/// For example, <see cref="IServiceProvider"/>
/// </summary>
public class MvvmObjectFactory : IMvvmObjectFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MvvmObjectFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    /// <summary>
    /// Returns an instance of the View with ViewModel as DataContext assigned
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="onViewCreated"></param>
    /// <param name="onViewModelCreated"></param>
    /// <returns></returns>
    public IZoneView Create<TView, TViewModel>(Action<TView>? onViewCreated = null, Action<TViewModel>? onViewModelCreated = null)
        where TView : IZoneView
        where TViewModel : IZoneViewModel
    {
        using var scope = _serviceProvider.CreateScope();

        var view = scope.ServiceProvider.GetRequiredService<TView>();
        onViewCreated?.Invoke(view);

        var viewModel = scope.ServiceProvider.GetRequiredService<TViewModel>();
        onViewModelCreated?.Invoke(viewModel);

        view.DataContext = viewModel;

        return view;
    }

    /// <summary>
    /// Returns an instance of the ViewType with instance of the ViewModel as DataContext property.
    /// </summary>
    /// <param name="viewType"></param>
    /// <param name="viewModelType"></param>
    /// <param name="onViewCreated"></param>
    /// <param name="onViewModelCreated"></param>
    /// <returns></returns>
    public IZoneView Create(Type viewType, Type viewModelType, Action<object>? onViewCreated = null, Action<object>? onViewModelCreated = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var view = (IZoneView)scope.ServiceProvider.GetRequiredService(viewType);
        onViewCreated?.Invoke(view);

        var viewModel = (IZoneViewModel)scope.ServiceProvider.GetRequiredService(viewModelType);
        onViewModelCreated?.Invoke(viewModel);

        view.DataContext = viewModel;

        return view;
    }
}
