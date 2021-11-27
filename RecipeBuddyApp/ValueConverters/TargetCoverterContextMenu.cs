using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class TargetCoverterContextMenu : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            return new object();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
