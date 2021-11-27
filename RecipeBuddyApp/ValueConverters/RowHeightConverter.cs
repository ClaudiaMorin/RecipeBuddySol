using System;
using Windows.UI.Xaml.Data;
using RecipeBuddy;

namespace RecipeBuddy.ValueConverters
{
    public class RowHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            //The parameters 1 and 0 deal with making the textbox visible or not as the parameter changes
            if (value != null && value.ToString().Length > 0)
            {
                return App.Current.Resources["RowVisible"];
            }

            //if (parameter != null && string.Compare(parameter.ToString().ToLower().Trim(), "textbox") == 1)
            //{
            //    return App.Current.Resources["RowVisible"];
            //}
            //if (parameter != null && string.Compare(parameter.ToString().ToLower().Trim(), "textbox") == 0)
            //{
            //    return App.Current.Resources["RowNotVisible"];
            //}

            //if (parameter != null && string.Compare(parameter.ToString().ToLower().Trim(), "textbox") == 1)
            //{
            //    return App.Current.Resources["RowVisible"];
            //}

            //This section deals with keeping empty rows from appearing.
            //if (value.ToString().Length > 0)
            //{
            //    return App.Current.Resources["RowVisible"];
            //}

            return App.Current.Resources["RowNotVisible"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
