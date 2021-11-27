using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    /// <summary>
    /// Takes the object and a max parameter, if the TryParse fails the Visibility will remain hidden
    /// if the object value is between 0 and the parameter number the Visibility will return visible
    /// </summary>
    public class IntToVisibilityWithParamsMaxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int bound;

            bool results = Int32.TryParse(parameter.ToString(), out bound);

            if(results == true)
            {
                if ((int)value < bound && (int)value > 0)
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
