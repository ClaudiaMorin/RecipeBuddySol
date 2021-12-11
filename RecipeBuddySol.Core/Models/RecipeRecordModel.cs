using Microsoft.Toolkit.Mvvm.ComponentModel;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Scrapers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RecipeBuddy.Core.Models
{
    public class RecipeRecordModel 
    {
        /// <summary>
        /// Creating a new RecipeModel using the information pulled from one source or another, this doesn't
        /// have any properties that can be used to link to datacontext, it is just the raw data
        /// </summary>
        /// <param name="RecipeBlurbModel">The source RecipeBlurbModel with all the information we need</param>
        public RecipeRecordModel(RecipeRecordModel reSource)
        {
            Description = reSource.Description;
            Title = reSource.Title;
            Author = reSource.Author;
            Website = "";
            Link = reSource.Link;
            TypeAsInt = reSource.TypeAsInt;
            ListOfIngredientStrings = reSource.ListOfIngredientStrings;
            ListOfDirectionStrings = reSource.ListOfDirectionStrings;
        }

        public RecipeRecordModel(RecipeDisplayModel reSource)
        {
            Description = reSource.Description;
            Title = reSource.Title;
            Author = reSource.Author;
            Website = "";
            Link = reSource.Link.ToString();
            TypeAsInt = (int)reSource.RecipeType;
            ListOfIngredientStrings = reSource.listOfIngredientStringsForDisplay;
            ListOfDirectionStrings = reSource.listOfDirectionStringsForDisplay;
        }

        /// <summary>
        /// the empty RecipeCardModel
        /// </summary>
        public RecipeRecordModel()
        {
            Title = "Search for your next recipe find!";
            Description = "";
            Author = "";
            Website = "";
            Link = null ;
            TypeAsInt = (int)Type_Of_Recipe.Unknown;

            ListOfIngredientStrings = new List<string>();
            ListOfDirectionStrings = new List<string>();
            ListOfDirectionStrings.Add("-Directions");
        }

        /// <summary>
        /// the Model used when we have an 
        /// </summary>
        public RecipeRecordModel(string ingredString, string descripString)
        {
            Title = "Search for your next recipe find!";
            Description = "";
            Author = "";
            Website = "";
            Link = null;
            TypeAsInt = (int)Type_Of_Recipe.Unknown;

            ListOfIngredientStrings = StringManipulationHelper.TurnStringintoListFromDB(ingredString);
            ListOfDirectionStrings = StringManipulationHelper.TurnStringintoListFromDB(descripString);
        }

        public RecipeRecordModel(List<string> ingredString, List<string> descripString)
        {
            Title = "Search for your next recipe find!";
            Description = "";
            Author = "";
            Website = "";
            Link = null;
            TypeAsInt = (int)Type_Of_Recipe.Unknown;

            ListOfIngredientStrings = ingredString;
            ListOfDirectionStrings = descripString;
        }


        public void CopyRecipeModel(RecipeRecordModel reSource)
        {
            Description = string.Copy(reSource.Description);
            Title = string.Copy(reSource.Title);
            Author = string.Copy(reSource.Author);
            Website = reSource.Website;
            Link = reSource.Link;
            TypeAsInt = reSource.TypeAsInt;
            ListOfIngredientStrings = reSource.ListOfIngredientStrings;
            ListOfDirectionStrings = new List<string>();
            ListOfDirectionStrings.Add("-Directions");
        }


        private int typeAsInt;
        public int TypeAsInt
        {
            get { return typeAsInt; }
            set { typeAsInt = value; }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string link;
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        //private Type_of_Websource website;
        //public Type_of_Websource Website
        //{
        //    get { return website; }
        //    set { website = value; }
        //}

        private string website;
        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        /// <summary>
        /// Formatted list of ingredients that includes the section headers and is sent to the property setter for the UI
        /// </summary>
        public List<string> ListOfIngredientStrings;

        public List<string> ListOfDirectionStrings;
    }
}
