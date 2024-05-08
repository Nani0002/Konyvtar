using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace konyvtar.View
{
    class PriviligeToRadioConverter : IValueConverter
    {
        //Allkalmazott jogának egyedbeli beállítása
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int i;
            if ((string)parameter == "Eladó")
            {
                i = 0;
            }
            else if ((string)parameter == "Admin")
            {
                i = 1;
            }
            else
            {
                i = 2;
            }
            return (int)value == i;
        }

        //Alkalmazott jogának megjelenítése
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((string)parameter == "Eladó")
            {
                return 0;
            }
            else if((string)parameter == "Admin")
            {
                return 1;
            }
            throw new ArgumentException("Hibás jog beálltás!");
        }
    }
}
