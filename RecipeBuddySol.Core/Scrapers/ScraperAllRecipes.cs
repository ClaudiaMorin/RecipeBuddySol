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
                HtmlNodeCollection list1 = doc.DocumentNode.SelectNodes("//a[@class='card__titleLink manual-link-behavior']");
                List<HtmlNode> list2 = new List<HtmlNode>();

                //Search didn't find anything!
                if (list1 == null || list1.Count == 0)
                {
                    return -1;
                }

                for (int count = 1; count < list1.Count; count++)
                {
                    str1 = list1[count].OuterHtml.Substring(list1[count].OuterHtml.IndexOf("href=")).Split('\"')[1];
                    if (str1 != str2)
                        if (listModel.URLLists.Add(new Uri(str1)) == -1)
                        {
                            return 0;
                        }

                    str2 = str1;
                }
                //foreach (var item in list1)
                //{
                //    str1 = item.OuterHtml.Substring(item.OuterHtml.IndexOf("href=")).Split('\"')[1];
                //    if (str1 != str2)
                //        if (listModel.URLLists.Add(str1) == -1)
                //        {
                //            return 0;
                //        }

                //    str2 = str1;
                //}
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
            directions.Add("-Direction");
            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;

            //List<string> directions = FillDirectionListAllRecipiesForRecipeEntry(doc, 30);

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);
            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='headline heading-content elementFont__display']", doc));
            recipeModel.Website = "AllRecipes";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(doc.DocumentNode.SelectSingleNode("//div[@class='recipe-summary']").InnerText);


            //recipeCardModel.TotalTime = GetTotalTime(doc.DocumentNode.SelectSingleNode("//div[@class='recipe-info-section']"));
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
            string headerTag = "-";

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-section']");

            try
            {
                string section_header = "Ingredients";
                ingredients.Add(headerTag + section_header);
                HtmlNodeCollection htmlNodes = ingred_node.SelectNodes("//*[@class='ingredients-item-name']");

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

        private static List<string> FillDirectionListAllRecipiesForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            string headerTag = "-";

            HtmlNode directions_top_level = doc.DocumentNode.SelectSingleNode("//ul[@class='instructions-section']");
            if (directions_top_level != null)
            {
                try
                {
                    directions.Add(headerTag + "Directions");
                    List<HtmlNode> groupslist = directions_top_level.SelectNodes("//div[@class='paragraph']").ToList<HtmlNode>();

                    foreach (HtmlNode section_node in groupslist)
                    {
                        directions.Add(StringManipulationHelper.CleanHTMLTags(section_node.InnerText));
                    }
                }

                catch (Exception e)
                { }
            }

            return Scraper.TrimListToSpecifiedEntries(30, directions);
        }

        
    }
}
