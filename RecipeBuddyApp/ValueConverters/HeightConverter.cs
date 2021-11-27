using System;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            //DockPanel DP = value as DockPanel;
            Double? HeightOfGrid = value as Double?;
            Double? HeightOfOffset = parameter as Double?;
            //Was the convertion good?
            if (HeightOfGrid != 0 && HeightOfGrid != null && HeightOfOffset != 0 && HeightOfOffset != null)
            {
                string s = (HeightOfGrid - HeightOfOffset).ToString();
                return s;
            }

            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
