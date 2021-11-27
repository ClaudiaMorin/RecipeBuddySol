using Windows.UI.Xaml.Data;
using System;


namespace RecipeBuddy.ValueConverters
{
    public class IntToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            int? items = value as int?;
            if (items != 0 && items != null)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }

    }
}
