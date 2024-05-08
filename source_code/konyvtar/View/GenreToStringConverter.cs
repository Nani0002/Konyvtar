using konyvtar.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace konyvtar.View
{
    class GenreToStringConverter : IValueConverter
    {
        //Könyv műfajának egyedbeli beállítása
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (Genre)Enum.Parse(typeof(Genre), (string)value);
            }
            catch
            {
                return null;
            }
        }

        //Könyv műfajának megjelenítése
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}
