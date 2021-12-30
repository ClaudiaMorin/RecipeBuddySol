using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.ValueConverters
{
    public class ButtonStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

            if (value != null && value.GetType() == typeof(TextBlock))
            {
                TextBlock textBlock = value as TextBlock;
                string alternateColor = parameter as string;

                //The parameters 1 and 0 deal with making the textbox visible or not as the parameter changes
                if (textBlock.Text.Length > 0)
                {
                    char myfirstChar = value.ToString()[0];
                    if (myfirstChar == '-' || myfirstChar == '!')
                    {
                        //reset so we start with white after the gray subheader
                        return App.Current.Resources["RowButtonNotVisible"];
                    }

                    if (string.Compare(alternateColor, "0") == 0)
                        return App.Current.Resources["BtnStyleUpdateEditRecipeItem"];

                    if (string.Compare(alternateColor, "1") == 0)
                        return App.Current.Resources["BtnStyleUpdateEditRecipeItem"];
                }
            }

            return App.Current.Resources["RowButtonNotVisible"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
