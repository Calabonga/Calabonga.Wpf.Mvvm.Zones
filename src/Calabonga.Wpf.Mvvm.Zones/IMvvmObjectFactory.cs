namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Factory for View and ViewModel generation from Dependency Container.
/// For example, <see cref="IServiceProvider"/>
/// </summary>
public interface IMvvmObjectFactory
{
    /// <summary>
    /// Returns an instance of the View with ViewModel as DataContext assigned
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <param name="onViewCreated"></param>
    /// <param name="onViewModelCreated"></param>
    /// <returns></returns>
    IZoneView Create<TView, TViewModel>(Action<TView>? onViewCreated = null, Action<TViewModel>? onViewModelCreated = null)
        where TView : IZoneView
        where TViewModel : IZoneViewModel;

    /// <summary>
    /// Returns an instance of the ViewType with instance of the ViewModel as DataContext property.
    /// </summary>
    /// <param name="viewType"></param>
    /// <param name="viewModelType"></param>
    /// <param name="onViewCreated"></param>
    /// <param name="onViewModelCreated"></param>
    /// <returns></returns>
    IZoneView Create(Type viewType, Type viewModelType, Action<object>? onViewCreated = null, Action<object>? onViewModelCreated = null);
}
