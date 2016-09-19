using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using SchProject.TechSupportSecure;

namespace SchProject.Resources.Layout.Converters
{
    public class TechnicianStatusToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TechnicianStatus selected = (TechnicianStatus)value;
            if (selected == TechnicianStatus.Available)
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