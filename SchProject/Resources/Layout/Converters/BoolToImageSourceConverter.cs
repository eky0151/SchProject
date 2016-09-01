using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace SchProject.Resources.Layout.Converters
{
    public class BoolToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? selected = value as bool?;
            if (selected.HasValue && selected.Value)
            {
                return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/available.png"));
            }
            return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/notavailable.png"));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
