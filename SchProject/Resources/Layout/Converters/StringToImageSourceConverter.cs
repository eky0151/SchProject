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
     class ImageFactory
    {
        public static ImageSource Image(string param,string value)
        {
            if(String.IsNullOrEmpty(value))
                return new BitmapImage(new Uri(@"pack://application:,,,/Resources/Layout/Images/user.png"));
            switch (param)
            {
                case "original":
                    return new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/original/" + value,UriKind.RelativeOrAbsolute));
                case "512":
                    return new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/512/" + value, UriKind.RelativeOrAbsolute));
                case "64":
                    return new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/64/" + value, UriKind.RelativeOrAbsolute));
                case "32":
                    return new BitmapImage(new Uri("https://techsupportfiles.blob.core.windows.net/images/32/" + value, UriKind.RelativeOrAbsolute));
                default:
                    return null;
            }
        }
    }  
    public class StringToImageSourceConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ImageFactory.Image((string)parameter, (string) value);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
