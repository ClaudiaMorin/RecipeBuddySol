
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Scrapers;
using Windows.Foundation;
using System.Threading;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RecipeBuddy.Core.Models
{
    public sealed class GenerateSearchResultsLists : ObservableObject
    {

        private static readonly GenerateSearchResultsLists instance = new GenerateSearchResultsLists();

        static GenerateSearchResultsLists()
        { }

        public static GenerateSearchResultsLists Instance
        {
            get { return instance; }
        }

        private GenerateSearchResultsLists()
        { }

        private string searchString { get; set; }

        /// <summary>
        /// The best way to enable/disable the search button is to make sure 3 searches have
        ///  taken place and then to enable the search after the last one finishes in BGWorker_EntryList_DoWork()
        /// </summary>
        public static int searchPanelCompleted;

        /// <summary>
        /// Foreach entry in the string search that we used on the website we are going to create
        ///an entry of type RecipeEntry in a public list called RecipeEntriesFromLastSearch 
        ///returns 3 once the search is finished
        /// </summary>
        /// <param name="SearchString">The string sent in to use to search the website</param>
        public static async Task<int> SearchSitesAndGenerateEntryList(string SearchString, RecipeListModel recipeListModel, Type_of_Websource websource, Action showCurrentEntry, CoreApplicationView view)
        {
            switch (websource)
            {
                
                case Type_of_Websource.AllRecipes:
                    {
                        //Failed Search
                        if (Scraper_AllRecipes_Southern_FoodAndWine.GenerateURLsListFromSearch(SearchString, recipeListModel, 0) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }

                
                case Type_of_Websource.SouthernLiving:
                    {
                        //Failed Search
                        if (Scraper_AllRecipes_Southern_FoodAndWine.GenerateURLsListFromSearch(SearchString, recipeListModel, 1) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }

               
                case Type_of_Websource.FoodAndWine:
                    {
                        // Failed Search
                        if (Scraper_AllRecipes_Southern_FoodAndWine.GenerateURLsListFromSearch(SearchString, recipeListModel, 2) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        };
                        break;
                    }
            }

            if (recipeListModel != null)
            {
                return FillBlurbList(recipeListModel, showCurrentEntry, view);
            }

            return -1;
        }


        /// <summary>
        /// Background worker thread is going to continue the job of taking the data out of the doc and
        /// converting it into something we can store in our RecipeEntriesList while the main thread 
        /// goes back to the UI
        /// </summary>
        private static int FillBlurbList(RecipeListModel recipeCardList, Action showCurrentEntry, Windows.ApplicationModel.Core.CoreApplicationView view)
        {
            Uri url;
            int UrlNum = recipeCardList.URLLists.URLListCount;
            
            RecipeRecordModel re = Scraper.ScrapeDataForRecipeEntry(recipeCardList.URLLists.RecipeURLsList[0]);

            //Gives us the first recipe to fill the blank panel and then the rest can happen async
            view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeCardList.Add(re));
            view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => showCurrentEntry());

            List<Task> TaskList = new List<Task>();

            for (int count = 1; recipeCardList.ListCount < RecipeURLLists.MaxEntries-1; count++)    
            {
                url = recipeCardList.URLLists.RecipeURLsList[count];
                if (url != null)
                {
                    re = Scraper.ScrapeDataForRecipeEntry(url);

                    //unlikely that we will get an odd recipe but if we do this is a second chance.
                    if (re != null)
                    {
                        try
                        {
                            //{"The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))"}
                            //AddToList(re); && recipeBlurbsList.AddToBlurbList(re);

                            //{"Element not found.\r\n\r\nElement not found.\r\n"} cant find GetCurrentView() but we are in a lib, not the UI.
                            //CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeBlurbsList.AddToBlurbList(re));

                            //Trying to send the correct view since I can't seem to find it?
                            //view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeCardList.Add(re));

                            TaskList.Add(Task.Run(() => view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeCardList.Add(re))));
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            return -1;
                        }
                    }
                }
            }

            Task t = Task.WhenAll(TaskList);
            return UrlNum;
        }


        private static void ScrapeAndAdd(Uri url, Action<RecipeRecordModel> AddToList)
        {
            RecipeRecordModel re = Scraper.ScrapeDataForRecipeEntry(url);
            CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => AddToList(re));
        }
    }
}

