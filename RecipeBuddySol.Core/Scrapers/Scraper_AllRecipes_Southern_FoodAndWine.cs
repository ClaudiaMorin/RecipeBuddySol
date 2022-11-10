using HtmlAgilityPack;
using System.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using System.Threading;
using System.Collections;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class Scraper_AllRecipes_Southern_FoodAndWine
    {
        private static readonly Scraper_AllRecipes_Southern_FoodAndWine instance = new Scraper_AllRecipes_Southern_FoodAndWine();

        static Scraper_AllRecipes_Southern_FoodAndWine()
        { }
        private Scraper_AllRecipes_Southern_FoodAndWine()
        { }

        public static Scraper_AllRecipes_Southern_FoodAndWine Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromSearch(string strSearch, RecipeListModel listModel, int website)
        {
            List<string> myQuery = new List<string>();
            string strQuery = "";
            if (website == 0)
            {
                strQuery = "https://www.allrecipes.com/search?q=";
            }
            if (website == 1)
            {
                strQuery = "https://www.southernliving.com/search?q=";
            }
            if (website == 2)
            {
                strQuery = "https://www.foodandwine.com/search?q=";
            }
            var web = new HtmlWeb();

            string[] myQueryArray = strSearch.Split(' ');
            strQuery += myQueryArray[0];

            for (int i = 1; i < myQueryArray.Length; i++)
            {
                strQuery += '+' + myQueryArray[i];
            }

            try
            {
                var doc = web.Load(strQuery);

                HtmlNode Node1 = doc.DocumentNode.SelectSingleNode("//div[@id='card-list_1-0']");
                HtmlNodeCollection list = Node1.SelectNodes("//a[@class='comp mntl-card-list-items mntl-document-card mntl-card card card--no-image']");

                //Search didn't find anything!
                if (list == null || list.Count == 0)
                {
                    return -1;
                }

                listModel.URLLists = new RecipeURLLists();

                if (website == 0)
                {
                    foreach (HtmlNode node in list)
                    {
                        string s = node.Attributes[4].Value;
                        if (s.Contains("https://www.allrecipes.com/recipe/"))
                            listModel.URLLists.Add(new Uri(s));
                    }
                }
                if (website == 1)
                {
                    foreach (HtmlNode node in list)
                    {
                        string s = node.Attributes[4].Value;
                        if (s.Contains("https://www.southernliving.com/recipes/"))
                            listModel.URLLists.Add(new Uri(s));
                    }
                }
                if (website == 2)
                {
                    foreach (HtmlNode node in list)
                    {
                        string s = node.Attributes[4].Value;
                        if (s.Contains("https://www.foodandwine.com/recipes/"))
                            listModel.URLLists.Add(new Uri(s));
                    }
                }
            }

            catch (Exception e)
            {
                return -1;
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
        public static RecipeRecordModel ProcessRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {
            //We are going to check for an ingredients tag, if it can't be found we don't have a recipe so abort.
            //if (doc.DocumentNode.SelectSingleNode("//ul[@class='mntl-structured-ingredients__list']") == null)
            //    return null;

            List<string> ingredients = FillIngredientListRecipeEntry(doc, 50);
            if(ingredients == null)
                return null;

            List<string> directions = FillDirectionsListRecipeEntry(doc, 30);

            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);

            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//h1[@id='article-heading_1-0']", doc));

            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(doc.DocumentNode.SelectSingleNode("//h2[@id='article-subheading_1-0']").InnerText);

            recipeModel.Author = "By: " + StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//a[@class='mntl-attribution__item-name']", doc));
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

        private static List<string> FillIngredientListRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//ul[@class='mntl-structured-ingredients__list']");

            if (ingred_node == null)
                return null;

            try
            {
                HtmlNodeCollection htmlNodes = ingred_node.SelectNodes("//li[@class='mntl-structured-ingredients__list-item ']");

                for (int i = 0; i < countList; i++)
                {
                    HtmlNode sectionHeader_node = htmlNodes[i];
                    ingredients.Add(StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText));
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(countList, ingredients);
        }

        private static List<string> FillDirectionsListRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            try
            {
                //HtmlNode direct_node = doc.DocumentNode.SelectSingleNode("//ol[@id='mntl-sc-block_2-0']");
                HtmlNode direct_node = doc.DocumentNode.SelectSingleNode("//div[@class='comp recipe__steps mntl-block']");
                //HtmlNodeCollection htmlNodes = direct_node.SelectNodes("//p[@class='comp mntl-sc-block mntl-sc-block-html']");
                HtmlNodeCollection htmlNodes = direct_node.SelectNodes("//li[@class='comp mntl-sc-block-group--LI mntl-sc-block mntl-sc-block-startgroup']");

                for (int i = 0; i < countList; i++)
                {
                    HtmlNode sectionHeader_node = htmlNodes[i];
                    directions.Add(StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText));
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(countList, directions);
        }

    }
}
