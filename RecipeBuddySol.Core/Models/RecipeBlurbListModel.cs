using System;
//using RecipeBuddy.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Core.Models
{
    public class RecipeBlurbListModel : ObservableObject
    {
        //List of the Recipe links generated from the user's search terms
        public ObservableCollection<RecipeBlurbModel> RecipiesBlurbList { get; set; }


        //The index of the next valid URL to be returned to a caller
        public RecipeURLLists URLLists;
        private int displayCurrentCardIndex = 1;


        public RecipeBlurbListModel()
        {
            RecipiesBlurbList = new ObservableCollection<RecipeBlurbModel>();
            URLLists = new RecipeURLLists();
            listCountOfBlurbs = 0;
            CurrentCardIndex = 0;
        }

        private string currentCardTitle;
        public string CurrentCardTitle
        {
            get { return currentCardTitle; }
            set { SetProperty(ref currentCardTitle, value); }
        }

        /// <summary>
        /// The CurrentCardIndex sets the recipe that has current focus
        /// </summary>
        private int currentCardIndex;
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
            private set
            {
                SetProperty(ref displayCurrentCardIndex, value);
            }
        }

        //Clears the RecipeEntries list if a new search term is used
        public void ClearList()
        {
            URLLists.ClearLists();
            URLLists.URLListCount = 0;
            RecipiesBlurbList.Clear();
            ListCountOfBlurbs = 0;
            CurrentCardIndex = 0;
        }


        /// <summary>
        /// Adds a new entry to the specified list but not the first entry
        /// </summary>
        /// <param name="RE">New Entry to our list</param>
        public void AddToBlurbList(RecipeBlurbModel RE)
        {
            //Check to see if we already have it in our list.
            foreach (var RC in RecipiesBlurbList)
            {
                //The entry isn't valid or we already have it return!
                if (RE.Description == null || RC.Description == RE.Description)
                {
                    return;
                }
            }

            RecipiesBlurbList.Add(RE);
            ListCountOfBlurbs = listCountOfBlurbs + 1;
        }

        /// <summary>
        /// Adds a new entry to the specified list but not the first entry
        /// </summary>
        /// <param name="RE">New Entry to our list</param>
        public void AddToBlurbListNoCheck(RecipeBlurbModel RE)
        {
            if (RE == null || RE.Description == null)
            {
                return;
            }

            RecipiesBlurbList.Add(RE);
            ListCountOfBlurbs = listCountOfBlurbs + 1;
        }

        /// <summary>
        /// Removes an entry to the specified list
        /// </summary>
        /// <param name="RE"></param>
        public void Remove(int currIndex)
        {
            RecipiesBlurbList.RemoveAt(currIndex);
        }

        /// <summary>
        /// Use with Logout - removes entire list
        /// sets the listcount=0
        /// </summary>
        public void RemoveAll()
        {
            RecipiesBlurbList.Clear();
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
        public RecipeBlurbModel GetNextEntryInLoop()
        {
            if (CurrentCardIndex + 1 == RecipiesBlurbList.Count)
                CurrentCardIndex = 0;

            else
                CurrentCardIndex++;

            return RecipiesBlurbList[CurrentCardIndex];
        }

        //Returns the next Entry in the list to the caller, if we are at the max
        //it will loop back to the begining.
        public RecipeBlurbModel GetCurrentEntry()
        {
            if (RecipiesBlurbList.Count > 0)
                return RecipiesBlurbList[CurrentCardIndex];
            else
                return null;
        }

        /// <summary>
        /// Accesses the list like an array and returns the item selected at the specific index
        /// </summary>
        /// <param name="index">The index of the item we are looking for</param>
        /// <returns></returns>
        public RecipeBlurbModel GetEntry(int index)
        {
            if (index > RecipiesBlurbList.Count)
            {
                //CurrentCardIndex = ListCount - 1;
                return RecipiesBlurbList[ListCountOfBlurbs - 1];
            }
            else
            {
                //CurrentCardIndex = index + 1;
                return RecipiesBlurbList[index];
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

            if (RecipiesBlurbList.Count > 0)
            {
                for (; count < RecipiesBlurbList.Count; count++)
                {
                    if (string.Compare(title, RecipiesBlurbList[count].Title) == 0)

                        return count;
                }
            }

            return -1;
        }


        //Returns the next Entry in the list to the caller, if we are at the max
        //it will loop back to the begining.
        public RecipeBlurbModel GetPreviousEntryInLoop()
        {
            if (currentCardIndex == 0)
                CurrentCardIndex = ListCountOfBlurbs - 1;

            else
                CurrentCardIndex--;

            return RecipiesBlurbList[CurrentCardIndex];
        }

        public bool IsFoundInList(RecipeBlurbModel recipeblurb)
        {
            foreach (RecipeBlurbModel recipe in RecipiesBlurbList)

                if (string.Compare(recipe.Title, recipeblurb.Title) == 0 && string.Compare(recipe.Description, recipeblurb.Description) == 0)
                    return true;

            return false;
        }

        private int listCountOfBlurbs;
        public int ListCountOfBlurbs
        {
            get { return listCountOfBlurbs; }
            set
            {
                SetProperty(ref listCountOfBlurbs, value);
            }
        }

        public int CurrentIndex
        {
            get { return currentCardIndex; }
            set { SetProperty(ref currentCardIndex, value); }
        }
    }
}
