using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SchProject.Resources.Layout.Converters
{
    class BoolToVisibilityConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isselected = (bool) values[0];
            string header = (string) values[1];
            if (!String.IsNullOrEmpty(header))
            {
                return isselected ? Visibility.Visible : Visibility.Hidden;
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
