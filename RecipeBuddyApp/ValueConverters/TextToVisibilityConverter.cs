using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class TextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var intergerVal = value.ToString();

            if (intergerVal.Length == 0)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
