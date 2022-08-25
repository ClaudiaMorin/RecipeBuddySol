using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;


namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterSearching : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            bool search = (bool)value;

            if (search == false)
                return App.Current.Resources["BtnStyleSearching"];

            return App.Current.Resources["BtnStyleSearch"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}

