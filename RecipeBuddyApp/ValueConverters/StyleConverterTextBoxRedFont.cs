using System;
using Windows.UI.Xaml.Data;


namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterTextBoxRedFont : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string alternateColor = parameter as string;

            if (string.Compare(alternateColor, "0") == 0)
                return App.Current.Resources["NormalTbxStyleRed"];

            return App.Current.Resources["NormalTbxStyleYellowRed"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }

    }
}
