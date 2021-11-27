using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class CheckBoxEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            string numChecked = value as string;
            bool bChecked = (bool)parameter;

            //if we get 3 selections and we aren't talking about something that is checked we will disable the check box
            if (string.Compare(numChecked, "3") == 0)
            {
                if (bChecked == true)
                    return "True";
                else
                    return "False";
            }
            
            //cause we haven't hit 3 selections yet!
            return "True";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
