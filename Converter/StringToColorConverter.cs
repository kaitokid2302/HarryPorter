using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace HarryPorter.Converters
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // _value is a copy of value
            var _value = value.ToString();
            // if _value is null or _ return transparent
            if (_value == null || _value == "_") return Colors.Transparent;
            // if _value contains multiple colors, return the first one before the comma
            if (_value.Contains(","))
            {
                _value = _value.Split(",")[0];
            }
            
            return _value?.ToString() switch
            {
                "green" => Colors.Green,
                "black" => Colors.Black,
                "red" => Colors.Red,
                "yellow" => Colors.Yellow,
                "blue" => Colors.Blue,
                "white" => Colors.White,
                "brown" => Colors.Brown,
                // all common colors
                "gray" => Colors.Gray,
                "blond" => Colors.BlanchedAlmond,
                "silver" => Colors.Silver,
                "scarlet" => Colors.Red,
                "" => Colors.Transparent,
                "hazel" => Colors.Brown,
                "pale" => Colors.White,
                "dark" => Colors.Black,
                "silvery" => Colors.Silver,
                "amber" => Colors.Orange,
                
                _ => Colors.Transparent
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "✓" : "✕";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Colors.Green : Colors.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}