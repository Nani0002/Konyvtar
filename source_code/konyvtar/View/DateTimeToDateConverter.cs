using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace konyvtar.View
{
    class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string year = ((DateTime)value).Year.ToString();
            string month;
            string day;
            if (((DateTime)value).Month < 10)
            {
                month = 0 + ((DateTime)value).Month.ToString();
            }
            else
            {
                month = ((DateTime)value).Month.ToString();
            }
            if (((DateTime)value).Day < 10)
            {
                day = 0 + ((DateTime)value).Day.ToString();
            }
            else
            {
                day = ((DateTime)value).Day.ToString();
            }
            return year+"-"+month+"-"+day;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] date = value.ToString().Split('-');
                return new DateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
            }
            catch
            {
                ErrorWindow w = new ErrorWindow("Hibás dátum formátum!", true);
                w.ShowDialog();
                return Binding.DoNothing;
            }
        }
    }
}
