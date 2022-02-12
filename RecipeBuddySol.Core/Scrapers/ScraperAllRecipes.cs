using HtmlAgilityPack;
using System.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using System.Threading;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class ScraperAllRecipes
    {
        private static readonly ScraperAllRecipes instance = new ScraperAllRecipes();

        static ScraperAllRecipes()
        { }
        private ScraperAllRecipes()
        { }

        public static ScraperAllRecipes Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromAllRecipesSearch(string strSearch, RecipeListModel listModel)
        {
            List<string> myQuery = new List<string>();

            string strQuery = "https://www.allrecipes.com/search/results/?search=";
            strQuery = String.Concat(strQuery, strSearch);
            var web = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = web.Load(strQuery);
            }

            catch (Exception e)
            {
                System.Net.WebRequest request = System.Net.WebRequest.Create(strQuery);
                request.Timeout = 1000;
                request.GetResponse();

                doc = web.Load(strQuery);
            }

            string str1 = null;
            string str2 = "";
            string str3 = "";
            HtmlNode Node1 = doc.DocumentNode.SelectSingleNode("//div[@class='search-results-content']");
            HtmlNodeCollection list1 = Node1.SelectNodes("//div[@class='component card card__recipe card__facetedSearchResult']");
            List<HtmlNode> list2 = new List<HtmlNode>();

                //Search didn't find anything!
                if (list1 == null || list1.Count == 0)
                {
                    return -1;
                }

            //we need to zero out all our lists.
            listModel.URLLists = new RecipeURLLists();

            for (int count = 0; count < list1.Count; count++)
                {
                str3 = list1[count].InnerHtml;

                str1 = str3.Substring(str3.IndexOf("href=")).Split('\"')[1];
                    if (str1 != str2)
                        if (listModel.URLLists.Add(new Uri(str1)) == -1)
                        {
                            return 0;
                        }

                    str2 = str1;
                }

                return 0;
        }

        /// <summary>
        /// Processes the xml following the AllRecipies website's specific tweeks that are nessesary to pull data
        /// </summary>
        /// <param name="recipeEntry">The entry we are stuffing this into</param>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="splitter">The the way to split up the description so we only keep two sentences</param>
        /// <param name="uri">website</param>
        public static RecipeRecordModel ProcessAllRecipesRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {

            List<string> ingredients = FillIngredientListAllRecipesForRecipeEntry(doc, 50);
            List<string> directions = new List<string>();

            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);
            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='headline heading-content elementFont__display']", doc));
            //recipeModel.Website = "AllRecipes";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(doc.DocumentNode.SelectSingleNode("//div[@class='recipe-summary elementFont__dek--paragraphWithin elementFont__dek--linkWithin']").InnerText);

            recipeModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//span[@class='author-name authorName linkHoverStyle']", doc));
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;
            return recipeModel;
        }

        /// <summary>
        /// Gets the Total Time it takes to complete the recipe
        /// </summary>
        /// <param name="HtmlNode">the HTML node that contains the time data</param>
        /// <returns>A string representing the total time the recipe takes to complete</returns>
        private static string GetTotalTime(HtmlNode topNode)
        {
            HtmlNode htmlNode = topNode.SelectSingleNode("//section[@class='recipe-meta-container two-subcol-content clearfix']");
            string[] data = htmlNode.InnerText.Split(' ');

            try
            {
                for (int count = 0; count < data.Length; count++)
                {
                    if (string.Compare(data[count].ToLower(), "total:") == 0)
                    {
                        return data[count + 1] + " " + data[count + 2];
                    }
                }
            }
            catch (Exception e)
            {
                return "";
            }

            return "";
        }

        private static List<string> FillIngredientListAllRecipesForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-section']");

            try
            {
                HtmlNodeCollection htmlNodes = ingred_node.SelectNodes("//*[@class='ingredients-item-name elementFont__body']");

                for (int i = 0; i < countList; i++)
                {
                    HtmlNode sectionHeader_node = htmlNodes[i];
                    ingredients.Add(StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText));
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(50, ingredients);
        }
        
    }
}
