using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class ReverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            string visValue = value.ToString();
            if (String.Compare(visValue.ToLower(), "visible") == 0)
            {
                return "Collapsed";
            }

            return "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
