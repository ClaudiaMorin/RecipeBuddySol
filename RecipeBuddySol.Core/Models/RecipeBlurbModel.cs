using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace RecipeBuddy.Core.Models
{
    public class RecipeBlurbModel : ObservableObject
    {
        /// <summary>
        /// Creating a new RecipeBlurbModel using the information from another one
        /// </summary>
        /// <param name="RecipeBlurbModel">The source RecipeBlurbModel with all the information we need</param>
        public RecipeBlurbModel(RecipeBlurbModel reSource)
        {
            Description = reSource.Description;
            Title = reSource.Title;
            Author = reSource.Author;
            Website = reSource.Website;
            Link = reSource.Link;
            Recipe_Type = reSource.Recipe_Type;
        }

        /// <summary>
        /// the empty RecipeCardModel is used with Dapper when loading information from the DB
        /// and when there is a null entry and a RecipePanel needs to be populated!
        /// </summary>
        public RecipeBlurbModel()
        {
            Title = "Search for your next recipe find!";
            Description = "";
            Author = "";
            Website = "";
            Link = "";
            Recipe_Type = Type_Of_Recipe.Unknown;
        }

        public void CopyRecipeBlurbModel(RecipeBlurbModel reSource)
        {
            Description = string.Copy(reSource.Description);
            Title = string.Copy(reSource.Title);
            Author = string.Copy(reSource.Author);
            Website = string.Copy(reSource.Website);
            Link = string.Copy(reSource.Link);
            Recipe_Type = reSource.Recipe_Type;
        }

        private Type_Of_Recipe type_of_Recipe;
        public Type_Of_Recipe Recipe_Type
        {
            get { return type_of_Recipe; }
            set { type_of_Recipe = value; }
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
            set {description = value;}
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
    }
}
