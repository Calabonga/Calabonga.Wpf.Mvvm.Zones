using System.Globalization;
using System.Windows.Data;

namespace Calabonga.Wpf.MvvmMdi.Core;

public class ZoneNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)parameter == (string)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? parameter : null;
    }
}