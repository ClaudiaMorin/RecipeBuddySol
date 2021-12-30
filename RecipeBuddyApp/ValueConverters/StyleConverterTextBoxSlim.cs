﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RecipeBuddy.ValueConverters
{
    class StyleConverterTextBoxSlim : IValueConverter
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
                    return App.Current.Resources["SubHeaderTbxStyle"];
                }

                if (myfirstChar == '!')
                    return App.Current.Resources["WarningTbxStyle"];
            }

            if (string.Compare(alternateColor, "0") == 0)
                return App.Current.Resources["NormalTbxStyleSlim"];

            return App.Current.Resources["NormalTbxStyleBlueSlim"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
