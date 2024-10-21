namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// ZoneViewModel interface
/// </summary>
public interface IZoneViewModel
{
    /// <summary>
    /// Returns reference to <see cref="IZoneManager"/> manager for current zone item ViewModel.
    /// </summary>
    IZoneManager ZoneManager { get; }

    /// <summary>
    /// Deactivates current viewModel
    /// </summary>
    void OnDeactivate();

    /// <summary>
    /// Activates current viewModel
    /// </summary>
    void OnActivate();
}
