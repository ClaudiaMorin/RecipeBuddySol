
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

namespace RecipeBuddy.Core.Models
{
    public sealed class GenerateSearchResultsLists : ObservableObjBase
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
        public static async Task<int> SearchSitesAndGenerateEntryList(string SearchString, RecipeBlurbListModel recipeBlurbsListModel, Type_of_Websource websource, Action showCurrentEntry, CoreApplicationView view)
        {
            switch (websource)
            {
                case Type_of_Websource.Epicurious:
                    {
                        //Failed Search
                        if (ScraperEpicurious.GenerateURLsListFromEpicuriousSearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }
                case Type_of_Websource.AllRecipes:
                    {
                        //Failed Search
                        if (ScraperAllRecipes.GenerateURLsListFromAllRecipesSearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }

                case Type_of_Websource.FoodNetwork:
                    {
                        //Failed Search
                        if (ScraperFoodNetwork.GenerateURLsListFromFoodNetworkSearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }
                case Type_of_Websource.SouthernLiving:
                    {
                        //Failed Search
                        if (ScraperSouthernLiving.GenerateURLsListFromSouthernLivingSearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }

                case Type_of_Websource.Tasty:
                    {
                        //Failed Search
                        if (ScraperTasty.GenerateURLsListFromTastySearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        }
                        break;
                    }
                case Type_of_Websource.FoodAndWine:
                    { //Failed Search

                        if (ScraperFoodAndWine.GenerateURLsListFromFoodAndWineSearch(SearchString, recipeBlurbsListModel) == -1)
                        {
                            Console.WriteLine("error in GenerateURLs");
                            return -1;
                        };
                        break;
                    }
            }

            if (recipeBlurbsListModel != null)
            {
                FillBlurbList(recipeBlurbsListModel, showCurrentEntry, view);
                return 0;
            }

            return -1;
        }


        /// <summary>
        /// Background worker thread is going to continue the job of taking the data out of the doc and
        /// converting it into something we can store in our RecipeEntriesList while the main thread 
        /// goes back to the UI
        /// </summary>
        private static int FillBlurbList(RecipeBlurbListModel recipeBlurbsList, Action showCurrentEntry, Windows.ApplicationModel.Core.CoreApplicationView view)
        {

            string url;
            int intRet = 0;
            int UrlNum = recipeBlurbsList.URLLists.URLListCount;

            RecipeBlurbModel re = Scraper.ScrapeDataForRecipeEntry(recipeBlurbsList.URLLists.RecipeURLsList[0]);
            view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeBlurbsList.AddToBlurbListNoCheck(re));
            view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => showCurrentEntry());

            for (int count = 1; count < UrlNum; count++)
            {
                url = recipeBlurbsList.URLLists.RecipeURLsList[count];
                if (url.Length > 1)
                {
                    re = Scraper.ScrapeDataForRecipeEntry(url);

                    //unlikely that we will get an odd recipe but if we do this is a second chance.
                    if (re != null)
                    {
                        try
                        {
                            //{"The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))"}
                            //AddToList(re); 

                            //{"The application called an interface that was marshalled for a different thread. (Exception from HRESULT: 0x8001010E (RPC_E_WRONG_THREAD))"}
                            //recipeBlurbsList.AddToBlurbList(re);

                            //{"Element not found.\r\n\r\nElement not found.\r\n"} cant find GetCurrentView() but we are in a lib, not the UI.
                            //CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeBlurbsList.AddToBlurbList(re));

                            //Trying to send the correct view since I can't seem to find it?
                            view.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => recipeBlurbsList.AddToBlurbListNoCheck(re));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                    }
                }
            }

            //Critical to prevent a user from running a second search while the first one is still going!
            //This should increment with every panel that runs a search and we have three panels.
            searchPanelCompleted++;
            //at this point we have run 3 searches which means we are done.
            //if (searchPanelCompleted == 3)
            //{
            //    searchPanelCompleted = 0;
            intRet = 3;
            //    //SearchViewModel.Instance.SearchEnabled = true;
            //}

            return intRet;
        }


        private static void ScrapeAndAdd(string url, Action<RecipeBlurbModel> AddToList)
        {
            RecipeBlurbModel re = Scraper.ScrapeDataForRecipeEntry(url);
            CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => AddToList(re));
        }
    }
}

