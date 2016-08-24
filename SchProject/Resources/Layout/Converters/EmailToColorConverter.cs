using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SchProject.Resources.Layout.Converters
{
    class EmailToColorConverter : IValueConverter
    {
        const string EMAILREGEX =
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO: check usre in database
            string text = value as string;
            if (text != null)
            {
                if (Regex.IsMatch(text, EMAILREGEX, RegexOptions.IgnoreCase))
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BEF8B0"));
                }
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0203D"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
