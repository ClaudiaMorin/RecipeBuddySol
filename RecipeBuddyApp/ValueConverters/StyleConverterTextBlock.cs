using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;

namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterTextBlock : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string alternateColor = parameter as string;

            if (value.ToString().Length > 0)
            {
                char myfirstChar = value.ToString()[0];
                if (myfirstChar == '-')
                {
                    //reset so we start with white after the gray subheader
                    return App.Current.Resources["SubHeaderTbkStyle"]; 
                }

                if (myfirstChar == '!')
                    return App.Current.Resources["WarningTbkStyle"]; 

                if (string.Compare(alternateColor, "0")==0)
                    return App.Current.Resources["NormalTbkStyle"];

                if (string.Compare(alternateColor, "1")==0)
                    return App.Current.Resources["NormalTbkStyleBlue"];
            }

            return App.Current.Resources["CollapsedTbkStyle"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
