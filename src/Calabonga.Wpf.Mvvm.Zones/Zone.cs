using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

public class Zone : IZone
{
    private readonly DependencyObject _control;

    private readonly List<ZoneItem> _views = new();

    public Zone(string mapName, DependencyObject element)
    {
        Name = mapName;
        _control = element;
    }

    public IEnumerable<ZoneItem> Views => _views;

    public string Name { get; }

    public ZoneItem CreateOrActivate<TZoneView>(TZoneView view, Action<ZoneItem> onActivating) where TZoneView : IZoneView
    {
        var viewInZone = _views.SingleOrDefault(x => x.Type == typeof(TZoneView));

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

    public ZoneItem? GetActive() => _views.SingleOrDefault(x => x.IsActive);

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
