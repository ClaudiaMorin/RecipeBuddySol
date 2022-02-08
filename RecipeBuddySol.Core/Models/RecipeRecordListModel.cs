using System;
using RecipeBuddy.Core.Helpers;
using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Core.Models
{

    /// <summary>
    /// Class which will contian and manage the Recipe list that is returned from the user's search query and the RecipeURLList.
    /// </summary>
    public class RecipeListModel : ObservableObject
    {

        //List of the Recipe links generated from the user's search terms
        public ObservableCollection<RecipeRecordModel> RecipesList { get; set; }

        //The index of the next valid URL to be returned to a caller
        public RecipeURLLists URLLists;
        private int displayCurrentCardIndex = 1;
        private int currentCardIndex;
        private string currentCardTitle;
        private int listCount;

        public RecipeListModel()
        {
            RecipesList = new ObservableCollection<RecipeRecordModel>();
            CurrentCardIndex = 0;
            ListCount = 0;
        }

        //Clears the RecipeEntries list if a new search term is used
        public void ClearList()
        {
            RecipesList = new ObservableCollection<RecipeRecordModel>();
            CurrentCardIndex = 0;
            ListCount = 0;
        }

        public void Add(RecipeRecordModel RE)
        {
            if (RE == null)
                return;

            //we just added the first entry to the list
            if (listCount == 0)
            {
                RecipesList.Add(RE);
                ListCount = ListCount + 1;
                return;
            }
            else
            {   //Check to see if we already have it in our list.
                foreach (var RC in RecipesList)
                {
                    if (RC.ListOfIngredientStrings == RE.ListOfIngredientStrings)
                        return;
                }
            }
            RecipesList.Add(RE);
            ListCount = ListCount + 1;
        }

        /// <summary>
        /// Removes an entry to the specified list
        /// </summary>
        /// <param name="RE"></param>
        public void Remove(int currIndex)
        {
            RecipesList.RemoveAt(currIndex);
            ListCount = ListCount - 1;
        }

        /// <summary>
        /// Use with Logout - removes entire list
        /// sets the listcount=0
        /// </summary>
        public void RemoveAll()
        {
            RecipesList.Clear();
            ListCount = 0;
        }
        /// <summary>
        /// Sets the current index equal to the title that is passed in, if it is found in the list
        /// </summary>
        /// <param name="title">The recipe title to set to the current recipe</param>
        public int SettingCurrentIndexByTitle(string title)
        {
            int entryIndex = GetEntryIndex(title);
            if (entryIndex != -1)
            {
                CurrentCardIndex = entryIndex;
                CurrentCardTitle = title;
                return entryIndex;
            }
            return -1;
        }

        //Returns the next Entry in the list to the caller, if we are at the max
        //it will loop back to the begining.
        public RecipeRecordModel GetNextEntryInLoop()
        {
            if (CurrentCardIndex + 1 == RecipesList.Count)
                CurrentCardIndex = 0;

            else
                CurrentCardIndex++;

            return RecipesList[CurrentCardIndex];
        }

        //Returns the next Entry in the list to the caller, if we are at the max
        //it will loop back to the begining.
        public RecipeRecordModel GetCurrentEntry()
        {
            if (RecipesList.Count > 0)
                return RecipesList[CurrentCardIndex];
            else
                return null;
        }

        /// <summary>
        /// Accesses the list like an array and returns the item selected at the specific index
        /// </summary>
        /// <param name="index">The index of the item we are looking for</param>
        /// <returns></returns>
        public RecipeRecordModel GetEntry(int index)
        {
            if (index > RecipesList.Count)
            {
                return RecipesList[ListCount-1];
            }
            else
            {
                return RecipesList[index];
            }
        }

        /// <summary>
        /// Accesses the list like an array and returns the index of the specific title
        /// </summary>
        /// <param name="string">The title of the item we are looking for</param>
        /// <returns></returns>
        public int GetEntryIndex(string title)
        {
            int count = 0;
            
            if (RecipesList.Count > 0)
            {
                for (; count < RecipesList.Count; count++)
                {
                    if (string.Compare(title, RecipesList[count].Title) == 0)

                        return count;
                }
            }
            return -1;
        }


        //Returns the next Entry in the list to the caller, if we are at the max
        //it will loop back to the begining.
        public RecipeRecordModel GetPreviousEntryInLoop()
        {
            if (currentCardIndex == 0)
                CurrentCardIndex = ListCount - 1;

            else
                CurrentCardIndex--;

            return RecipesList[CurrentCardIndex];
        }

        public bool IsFoundInList(RecipeRecordModel recipeModel)
        {
            foreach (RecipeRecordModel recipe in RecipesList)
                if (string.Compare(recipe.Title, recipeModel.Title) == 0)
                 return true;

            return false;
        }

        #region properties

        public int ListCount
        {
            get { return listCount; }
            set { SetProperty(ref listCount, value); }
        }

        public int CurrentIndex
        {
            get { return currentCardIndex; }
            set { SetProperty(ref currentCardIndex, value); }
        }

        public string CurrentCardTitle
        {
            get { return currentCardTitle; }
            private set { SetProperty(ref currentCardTitle, value); }
        }

        /// <summary>
        /// The CurrentCardIndex sets the recipe that has current focus
        /// </summary>
        public int CurrentCardIndex
        {
            get { return currentCardIndex; }
            set
            {
                SetProperty(ref currentCardIndex, value);
                DisplayCurrentCardIndex = currentCardIndex + 1;
            }
        }

        public int DisplayCurrentCardIndex
        {
            get { return displayCurrentCardIndex; }
            set { SetProperty(ref displayCurrentCardIndex, value); }
        }
       

        #endregion

    }
}
