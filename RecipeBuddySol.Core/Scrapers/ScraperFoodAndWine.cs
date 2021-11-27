using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class ScraperFoodAndWine
    {
        private static readonly ScraperFoodAndWine instance = new ScraperFoodAndWine();

        static ScraperFoodAndWine()
        { }
        private ScraperFoodAndWine()
        { }

        public static ScraperFoodAndWine Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromFoodAndWineSearch(string strSearch, RecipeBlurbListModel listModel)
        {
            List<string> myQuery = new List<string>();
            string strQuery = "https://www.foodandwine.com/search?q=";
            string[] myQueryArray = strSearch.Split(' ');

            foreach (var item in myQueryArray)
            {
                strQuery += item + '+';
            }
            strQuery = strQuery.TrimEnd('+');
            strQuery = String.Concat(strQuery, strSearch);
            var web = new HtmlWeb();

            try
            {
                var doc = web.Load(strQuery);
                HtmlNode searchResultsNode = doc.DocumentNode.SelectSingleNode("//div[@class='search-results-content-results-wrapper']");
                //Search didn't find anything!
                if (searchResultsNode == null)
                {
                    return -1;
                }

                HtmlNodeCollection list = searchResultsNode.SelectNodes("//div[@class='search-result']");

                //Search didn't find anything!
                if (list == null || list.Count == 0)
                {
                    return -1;
                }

                //listModel.URLLists.ClearLists();

                string firstStr;
                string secondStr;

                //skip the first it could be an article
                for (int itemCount = 0; itemCount < list.Count; itemCount++)
                {
                    firstStr = list[itemCount].OuterHtml;
                    if (firstStr.Contains("https://www.foodandwine.com/recipes/"))
                    {
                        secondStr = firstStr.Substring(firstStr.IndexOf("https:"));
                        firstStr = secondStr.Substring(0, secondStr.IndexOf(">")-1);
                        listModel.URLLists.Add(firstStr);
                    }
                }
                return 0;
            }

            catch (Exception e)
            {
                return -1;
            }
        }

        /// <summary>
        /// Processes the xml following the AllRecipies website's specific tweeks that are nessesary to pull data
        /// </summary>
        /// <param name="recipeEntry">The entry we are stuffing this into</param>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="splitter">The the way to split up the description so we only keep two sentences</param>
        /// <param name="uri">website</param>
        public static RecipeBlurbModel ProcessFoodAndWineRecipeType(HtmlDocument doc, char[] splitter, string uri)
        {

            List<string> ingredients = FillIngredientListFoodAndWineForRecipeEntry(doc, 50);
            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;

            List<string> directions = FillDirectionListFoodAndWineForRecipeEntry(doc, 30);

            RecipeBlurbModel recipeBlurbModel = new RecipeBlurbModel();


            recipeBlurbModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='headline heading-content elementFont__display']", doc));
            recipeBlurbModel.Website = "Food&Wine";
            recipeBlurbModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='recipe-summary elementFont__dek--paragraphWithin elementFont__dek--linkWithin']", doc));
            //recipeBlurbModel.TotalTime = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='recipe-meta-item-body']", doc));
            recipeBlurbModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//span[@class='author-name authorName linkHoverStyle']", doc));
            recipeBlurbModel.Link = uri;
            recipeBlurbModel.Recipe_Type = Scraper.FillTypeForRecipeEntry(recipeBlurbModel.Title);

            return recipeBlurbModel;
        }

        private static List<string> FillIngredientListFoodAndWineForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//fieldset[@class='ingredients-section__fieldset']");

            try
            {
                string section_header = "Ingredients";
                ingredients.Add(headerTag + section_header);
                HtmlNode htmlNode = ingred_node.SelectSingleNode("//ul[@class='ingredients-section']");
                HtmlNodeCollection htmlNodes = htmlNode.SelectNodes("//li[@class='ingredients-item']");
                string node;
                for (int i = 0; i < htmlNodes.Count; i++)
                {
                    node = htmlNodes[i].InnerText;

                    if (node != null)
                    {
                        string s1 = StringManipulationHelper.CleanHTMLTags(node);
                        ingredients.Add(s1);
                    }
                }
                
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(50, ingredients);
        }

        private static List<string> FillDirectionListFoodAndWineForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            HtmlNode directions_top_level = doc.DocumentNode.SelectSingleNode("//ul[@class='instructions-section']");
            
            if (directions_top_level != null )
            {
                try
                {
                    HtmlNodeCollection htmlNodes = directions_top_level.SelectNodes("//li[@class='subcontainer instructions-section-item']");
                    directions.Add("-Directions");
                    foreach (HtmlNode section_node in htmlNodes)
                    {
                        string str1 = section_node.OuterHtml;
                        string str2 = str1.Substring(str1.IndexOf("<p>") + 3);
                        str1 = str2.Substring(0, str2.IndexOf("</p>"));
                        directions.Add(StringManipulationHelper.CleanHTMLTags(str1));
                    }
                }

                catch (Exception e)
                { }
            }

            return Scraper.TrimListToSpecifiedEntries(30, directions);
        }

    }
}
