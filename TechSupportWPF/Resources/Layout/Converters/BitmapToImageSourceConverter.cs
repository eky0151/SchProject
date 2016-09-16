using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SchProject.Resources.Layout.Converters
{
    [ValueConversion(typeof(System.Drawing.Image), typeof(System.Windows.Media.ImageSource))]
    class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if(value == null)
            //{
            //    return null;
            //}

            //using (MemoryStream memory = new MemoryStream())
            //{
            //    ((System.Drawing.Bitmap)value).Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
            //    memory.Position = 0;
            //    BitmapImage bitmapimage = new BitmapImage();
            //    bitmapimage.BeginInit();
            //    bitmapimage.StreamSource = memory;
            //    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
            //    bitmapimage.EndInit();

            //    return bitmapimage;
            //}

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
