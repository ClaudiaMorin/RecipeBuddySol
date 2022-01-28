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
            ListOfIngredientStrings = new List<string>(reSource.ListOfIngredientStrings);
            ListOfDirectionStrings = new List<string>(reSource.ListOfDirectionStrings);
        }

        public RecipeRecordModel(RecipeDisplayModel reSource)
        {
            Description = String.Copy(reSource.Description);
            Title = String.Copy(reSource.Title);
            Author = String.Copy(reSource.Author);
            Website = "";
            if (reSource.Link != null)
                Link = String.Copy(reSource.Link.ToString());
            TypeAsInt = (int)reSource.RecipeType;
            ListOfIngredientStrings = new List<string>(reSource.listOfIngredientStringsForDisplay);
            ListOfDirectionStrings = new List<string>(reSource.listOfDirectionStringsForDisplay);
        }

        public RecipeRecordModel(string title, RecipeDisplayModel reSource)
        {
            Description = String.Copy(reSource.Description);
            Title = title;
            Author = String.Copy(reSource.Author);
            Website = "";
            if(reSource.Link != null)
            Link = String.Copy(reSource.Link.ToString());
            TypeAsInt = (int)reSource.RecipeType;
            ListOfIngredientStrings = new List<string> (reSource.listOfIngredientStringsForDisplay);
            ListOfDirectionStrings = new List<string> (reSource.listOfDirectionStringsForDisplay);
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
            ListOfIngredientStrings = new List<string>(StringManipulationHelper.TurnStringintoListFromDB(ingredString));
            ListOfDirectionStrings = new List<string>(StringManipulationHelper.TurnStringintoListFromDB(descripString));
        }

        public RecipeRecordModel(List<string> ingredString, List<string> descripString)
        {
            Title = "Search for your next recipe find!";
            Description = "";
            Author = "";
            Website = "";
            Link = null;
            TypeAsInt = (int)Type_Of_Recipe.Unknown;
            ListOfIngredientStrings = new List<string>(ingredString);
            ListOfDirectionStrings = new List<string>(descripString);
        }


        public void CopyRecipeModel(RecipeRecordModel reSource)
        {
            Description = string.Copy(reSource.Description);
            Title = string.Copy(reSource.Title);
            Author = string.Copy(reSource.Author);
            Website = reSource.Website;
            Link = reSource.Link;
            TypeAsInt = reSource.TypeAsInt;
            ListOfIngredientStrings = new List<string>(reSource.ListOfIngredientStrings);
            ListOfDirectionStrings = new List<string>();
        }

        public void CopyRecipeModel(RecipeDisplayModel reSource)
        {
            Description = string.Copy(reSource.Description);
            Title = string.Copy(reSource.Title);
            Author = string.Copy(reSource.Author);
            Website = reSource.Website;
            Link = "";
            TypeAsInt = (int)reSource.RecipeType;
            ListOfIngredientStrings = new List<string>(reSource.listOfIngredientStringsForDisplay);
            ListOfDirectionStrings = new List<string>(reSource.listOfDirectionStringsForDisplay);
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
