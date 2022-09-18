using System;
using System.Globalization;
using System.Windows.Data;

namespace wdskills.WPF.Core
{
    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = int.TryParse(value.ToString(), out int valueInt);
            if (result)
            {
                return valueInt != 0;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("IsNullConverter can only be used OneWay.");
        }
    }
}
