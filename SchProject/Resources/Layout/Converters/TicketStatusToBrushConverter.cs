using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using SchProject.Resources.Layout.StyleResources;

namespace SchProject.Resources.Layout.Converters
{
    class TicketStatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TicketStatus stat;
            bool success=Enum.TryParse((string) value, out stat);
            if (success)
            {
                switch (stat)
                {
                    case TicketStatus.New:
                        return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#89B1FA"));
                    case TicketStatus.Solved:
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
