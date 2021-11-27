using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;


namespace RecipeBuddy.ValueConverters
{
    public class StyleConverterUnhideFirstEmptyTextBox : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string alternateColor = parameter as string;

            if (value.ToString().Length == 0)
            { 
                return App.Current.Resources["NormalTbxStyle"]; 
            }

            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}

