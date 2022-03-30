using System;
using System.Globalization;
using System.Windows.Data;

namespace ClimaApp.ViewModel.ValueConverter
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool)value;
            if (isRaining)
                return "Chuva corrente";

            return "Sem chuvas ";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string isRaining = (string)value;

            if (isRaining == "Currently raining")
                return true;

            return false;

        }
    }
}
