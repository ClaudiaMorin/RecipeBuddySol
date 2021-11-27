using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class ButtonHeightInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            if (string.Compare(value.ToString().ToLower(), "Auto") == 0 )
            {
                return "Collapsed";
            }

            return "IsVisable";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
