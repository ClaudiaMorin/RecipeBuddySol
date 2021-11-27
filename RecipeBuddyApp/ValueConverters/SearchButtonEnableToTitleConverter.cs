using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.ValueConverters
{

        public class SearchButtonEnabledToTitleConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string culture)
            {
                Button button = (Button)value;
                if (button.IsEnabled)
                    return "Search";
                else
                    return "Searching...";
            }

            public object ConvertBack(object value, Type targetType, object parameter, string culture)
            {
                throw new NotImplementedException();
            }
        }

}
