using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace konyvtar.View
{
    class AmountToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Könyvek mennyiségének a listában való megjelenítése
            if((int)value == 0)
            {
                return Brushes.Red;
            }
            else if ((int)value < 5)
            {
                return Brushes.Salmon;
            }
            else if((int)value < 10)
            {
                return Brushes.LightYellow;
            }
            return Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
