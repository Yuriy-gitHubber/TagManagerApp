using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace TagManagerApp
{
    public class TypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as Type;

            if (type == typeof(int))
            {
                return new BitmapImage(new Uri("Images/int.png", UriKind.Relative));
            }
            if (type == typeof(double))
            {
                return new BitmapImage(new Uri("Images/double.png", UriKind.Relative));
            }
            if (type == typeof(bool))
            {
                return new BitmapImage(new Uri("Images/bool.png", UriKind.Relative));
            }

            return new BitmapImage(new Uri("Images/default.png", UriKind.Relative));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
