using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.ValueConverters
{
    public class RowHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                string text = value as string;

                //The parameters 1 and 0 deal with making row visible or not as the parameter changes
                if (text.Length < 1)
                {
                    return "0";
                }
            }
            return "Auto";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
