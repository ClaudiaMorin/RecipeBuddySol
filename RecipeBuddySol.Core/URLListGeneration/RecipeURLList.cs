using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;

namespace RecipeBuddy.Core.Models
{
    /// <summary>
    /// Contian and manage the URL list that is returned from the user's search query.
    /// </summary>
    public class RecipeURLLists : ObservableObject
    {
        //MaxEntries are the limit we can place on how many URL recipe links the list is allow to contain
        public static readonly int MaxEntries = 20;

        //List of the Recipe links generated from the user's search terms
        public List<Uri> RecipeURLsList;

        //The index of the next valid URL to be returned to a caller
        private int RecipeURLNextIndex;

        //this contructor will be called if there is no existing instance of the class
        public RecipeURLLists()
        {
            uRLListCount = 0;
            RecipeURLsList = new List<Uri>();
            RecipeURLNextIndex = 0;
        }

        ////Clears the URL list if a new search term is used
        public void ClearLists()
        {
            RecipeURLsList.Clear();
            uRLListCount = 0;
            RecipeURLNextIndex = 0;
        }

        //Checks to verify that there is still space under the 
        //MaxEntries Cap and if so it adds the new URL and returns 0
        //if not it returns -1
        public int Add(Uri url)
        {
            if (URLListCount >= MaxEntries )
            {
                return -1;
            }

            RecipeURLsList.Add(url);
            URLListCount++;
            return 0;
        }

        //Returns the next URL in the list to the caller, if there isn't another URL
        // because we are at the limit of the list we will return a blank string
        public Uri GetNextURL()
        {
            if (RecipeURLNextIndex >= RecipeURLsList.Count)
            {
                return null;
            }

            Uri strReturn = RecipeURLsList[RecipeURLNextIndex];
            RecipeURLNextIndex++;

            return strReturn;
        }

        //Returns the next URL in the list to the caller, if there isn't another URL
        // because we are at the limit of the list we will return a blank string
        public Uri GetNextAllRecipesURL()
        {
            if (RecipeURLNextIndex >= RecipeURLsList.Count)
            {
                return null;
            }

            Uri strReturn = RecipeURLsList[RecipeURLNextIndex];
            RecipeURLNextIndex++;

            return strReturn;
        }

        private int uRLListCount;
        public int URLListCount 
        { 
            get { return uRLListCount; }
            set { SetProperty(ref uRLListCount, value); }
        }

        public string ListCountAsString
        {
            get { return URLListCount.ToString(); }
        }
    }
}
