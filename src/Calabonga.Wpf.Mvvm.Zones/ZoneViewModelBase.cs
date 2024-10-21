using CommunityToolkit.Mvvm.ComponentModel;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Default implementation for <see cref="IZoneViewModel"/>
/// </summary>
public abstract class ZoneViewModelBase : ObservableObject, IZoneViewModel
{
    /// <summary>
    /// Returns reference to <see cref="IZoneManager"/> manager for current zone item ViewModel.
    /// </summary>
    public IZoneManager ZoneManager { get; set; } = null!;

    /// <summary>
    /// Deactivates current viewModel
    /// </summary>
    public virtual void OnDeactivate() { }

    /// <summary>
    /// Activates current viewModel
    /// </summary>
    public virtual void OnActivate() { }
}
