using System;
using System.Globalization;
using System.Windows.Data;
using xave.generator.test.Model;

namespace xave.generator.test.Converter
{
    public class CodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || value == null) return null;

            var code = (Code)value;
            string text = string.Empty;

            if (parameter.ToString().Contains("TYPE"))
                text = "[" + code.type + "] ";
            if (parameter.ToString().Contains("CODE"))
                text = text + "(" + code.code + ") ";
            if (parameter.ToString().Contains("NAME"))
                text = text + code.name;

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    } 
}
