using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

/// <summary>
/// Represents a Zone where UserControl placed
/// </summary>
public class Zone : IZone
{
    private readonly DependencyObject _control;

    private readonly List<ZoneItem> _views = new();

    public Zone(string mapName, DependencyObject element)
    {
        Name = mapName;
        _control = element;
    }

    /// <summary>
    /// Views in current zone
    /// </summary>
    public IEnumerable<ZoneItem> Views => _views;

    /// <summary>
    /// Current zone name
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Activates <see cref="IZoneView"/> already exists in the current zone or
    /// Creates a new instance of the view and activate it.
    /// </summary>
    /// <typeparam name="TZoneView"></typeparam>
    /// <param name="view"></param>
    /// <param name="onActivating"></param>
    /// <returns></returns>
    public ZoneItem CreateOrActivate<TZoneView>(TZoneView view, Action<ZoneItem> onActivating) where TZoneView : IZoneView
    {
        var viewInZone = _views.SingleOrDefault(x => x.Type == view.GetType());

        if (viewInZone is null)
        {
            if (view is not FrameworkElement userControl)
            {
                throw new InvalidOperationException($"Unable to activate zone {Name} with {view.GetType()}");
            }

            viewInZone = new ZoneItem(typeof(TZoneView), userControl);
            _views.Add(viewInZone);
        }

        SetContent(viewInZone, onActivating);

        return viewInZone;
    }

    /// <summary>
    /// Returns Active view from the views for current Zone
    /// </summary>
    /// <returns></returns>
    public ZoneItem? GetActive() => _views.SingleOrDefault(x => x.IsActive);

    /// <summary>
    /// Removes <see cref="ZoneItem"/>
    /// </summary>
    /// <param name="zoneItem"></param>
    public void RemoveItem(ZoneItem zoneItem)
    {
        zoneItem.DeactivateView();
        _views.Remove(zoneItem);
        _control.SetValue(ContentControl.ContentProperty, null);
    }

    private void SetContent(ZoneItem zoneItem, Action<ZoneItem> onActivating)
    {
        onActivating(zoneItem);
        zoneItem.ActivateView();
        _control.SetValue(ContentControl.ContentProperty, zoneItem.Content);
    }
}
