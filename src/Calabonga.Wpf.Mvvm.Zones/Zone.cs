using System.Windows;
using System.Windows.Controls;

namespace Calabonga.Wpf.Mvvm.Zones;

public class Zone : IZone
{
    private readonly DependencyObject _control;

    private readonly List<ZoneView> _views = new();

    public Zone(string mapName, DependencyObject element)
    {
        Name = mapName;
        _control = element;
    }

    public string Name { get; }

    public void CreateOrActivate<TZoneView>(TZoneView view) where TZoneView : IZoneView
    {
        var viewInZone = _views.SingleOrDefault(x => x.Type == typeof(TZoneView));

        if (viewInZone is null)
        {
            if (view is not FrameworkElement userControl)
            {
                throw new InvalidOperationException($"Unable to activate zone {Name} with {view.GetType()}");
            }

            viewInZone = new ZoneView(typeof(TZoneView), userControl);
            _views.Add(viewInZone);

        }

        SetContent(viewInZone);
    }

    public ZoneView? GetActive() => _views.SingleOrDefault(x => x.IsActive);

    private void SetContent(ZoneView view)
    {
        view.ActivateView();
        _control.SetValue(ContentControl.ContentProperty, view.View);
    }
}
