using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace finalProject
{
    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Appointment))
            {
                return Brushes.White;
            }
            Appointment ap = (Appointment)value;


            if (((Vehicle)ap.VehicleData).VehiclePickUp == "Vehicle Pickup service is required")
            {
                return Brushes.Red;
            }
            else
            {
                return Brushes.White;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
