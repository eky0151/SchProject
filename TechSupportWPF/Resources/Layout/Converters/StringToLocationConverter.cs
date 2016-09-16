using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using Microsoft.Maps.MapControl.WPF;

namespace SchProject.Resources.Layout.Converters
{
    class StringToLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            string loc = (string)value;
            if (loc.Length > 2)
            {
                var data=loc.Split('$');
                float lat;
                float longi;
                float.TryParse(data[0],out lat);
                float.TryParse(data[1], out longi);
                return new Location(lat,longi);
            }
            return new Location(44, 33);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
