using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;

namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterTextBlockRedFontCenterJustified : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            string alternateColor = parameter as string;

            if (value != null && value.ToString().Length > 0)
            {
                char myfirstChar = value.ToString()[0];

                if (string.Compare(alternateColor, "0") == 0)
                    return App.Current.Resources["CenterJustifiedTbkStyleRed"];

                if (string.Compare(alternateColor, "1") == 0)
                    return App.Current.Resources["CenterJustifiedTbkStyleYellowRed"];
            }

            return App.Current.Resources["CollapsedTbkStyle"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }

    }
}
