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
    class PriviligeToBrushConverter : IValueConverter
    {
        //Alkalamazott jogának listában való megjelenítése
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int priv = int.Parse(value.ToString());
            switch (priv)
            {
                case 0:
                    //Eladó
                    return Brushes.Gray;
                case 1:
                    //Admin
                    return Brushes.Orange;
                default:
                    //Hibás
                    return Brushes.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
