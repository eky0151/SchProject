using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SchProject.Resources.Layout.Converters
{
    public class NumberToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? count = value as int?;
            if (count != null && count == 1)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FBFBFB"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
