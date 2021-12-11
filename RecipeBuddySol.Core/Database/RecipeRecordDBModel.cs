using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBuddy.Core.Database
{
    class RecipeRecordDBModel
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public string Link { get; set; }
        public string Website { get; set; }
        public int TypeAsInt { get; set; }
        public string StringOfIngredientForListFromDB { get; set; }
        public string StringOfDirectionsForListFromDB { get; set; }
        public int UserID { get; set; }
    }
}
