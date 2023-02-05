using System;
using System.Globalization;
using System.Windows.Data;

namespace TLEMAITRE1_nasa
{
    // Convert a height to a percent of the screen
    public class HeightToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double height && parameter is double percent)
            {
                return height * percent;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
