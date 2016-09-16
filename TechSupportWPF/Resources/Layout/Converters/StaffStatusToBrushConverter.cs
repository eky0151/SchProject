using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using SchProject.Resources.Layout.StyleResources;
using SchProject.TechSupportSecure;

namespace SchProject.Resources.Layout.Converters
{
    class StaffStatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Status? stat = value as Status?;
            if (stat.HasValue)
            {
                switch (stat)
                {
                    case Status.Working:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#73D673"));
                }
            }
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF839E"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
