using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class IntToVisibilityWithParamsMinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int bound;

            bool results = Int32.TryParse(parameter.ToString(), out bound);

            if (results == true)
            {
                if ((int)value > bound && (int)value > 0)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
