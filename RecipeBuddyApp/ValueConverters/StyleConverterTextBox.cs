using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;


namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterTextBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string alternateColor = parameter as string;

            if (value.ToString().Length > 1)
            {
                char myfirstChar = value.ToString()[0];
                if (myfirstChar == '-')
                {
                    //reset so we start with white after the gray subheader
                    return App.Current.Resources["SubHeaderTbxStyle"];
                }

                if (myfirstChar == '!')
                    return App.Current.Resources["WarningTbxStyle"];
            }

            if (string.Compare(alternateColor, "0") == 0)
                return App.Current.Resources["NormalTbxStyle"];

            return App.Current.Resources["NormalTbxStyleBlue"];

        }

            public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
