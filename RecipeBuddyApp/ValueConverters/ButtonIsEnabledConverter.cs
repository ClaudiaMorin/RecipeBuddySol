using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class ButtonIsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            if (parameter == null)
                return "False";

            if (string.Compare(value.ToString().ToLower(), "Auto") == 0 && string.Compare(parameter.ToString().ToLower(), "true") == 0)
            {
                return "True";
            }
            if (string.Compare(value.ToString().ToLower(), "*") == 0 && string.Compare(parameter.ToString().ToLower(), "true") == 0)
            {
                return "True";
            }

            if (string.Compare(value.ToString().ToLower(), "0") == 0 && string.Compare(parameter.ToString().ToLower(), "false") == 0)
            {
                return "True";
            }

            return "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}