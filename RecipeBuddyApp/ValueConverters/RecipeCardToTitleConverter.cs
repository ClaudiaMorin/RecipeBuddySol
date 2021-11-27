using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Models;

namespace RecipeBuddy.ValueConverters
{
    public class RecipeCardToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {

           ObservableCollection<RecipeCardModel> recipeCards = (ObservableCollection<RecipeCardModel>)value;
           if (recipeCards.Count != 0)
           {
                return null; // recipeCard.Title; 
           }


            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
