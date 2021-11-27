using System;
using RecipeBuddy;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class ButtonIsVisibleRedFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            string alternateColor = parameter as string;

            if (value.ToString().Length > 0)
            {
                char myfirstChar = value.ToString()[0];
                if (myfirstChar == '-' || myfirstChar == '!')
                {
                    //reset so we start with white after the gray subheader
                    return RecipeBuddy.App.Current.Resources["RowButtonNotVisible"];
                }

                if (string.Compare(alternateColor, "0") == 0)
                    return App.Current.Resources["RowButtonVisibleWhiteRedFont"];

                if (string.Compare(alternateColor, "1") == 0)
                    return App.Current.Resources["RowButtonVisibleColorRedFont"];
            }

            return App.Current.Resources["RowButtonNotVisible"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
