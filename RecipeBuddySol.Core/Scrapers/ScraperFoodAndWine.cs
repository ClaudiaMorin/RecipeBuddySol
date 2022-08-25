using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

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
        public static int GenerateURLsListFromFoodAndWineSearch(string strSearch, RecipeListModel listModel)
        {
            List<string> myQuery = new List<string>();
            string strQuery = "https://www.foodandwine.com/search?q=";
            string[] myQueryArray = strSearch.Split(' ');

            foreach (var item in myQueryArray)
            {
                strQuery += item + '+';
            }
            strQuery = strQuery.TrimEnd('+');
            var web = new HtmlWeb();



            try
            {
                var doc = web.Load(strQuery);
                HtmlNode searchResultsNode = doc.DocumentNode.SelectSingleNode("//div[@id='card-list_1-0']");
                //Search didn't find anything!
                if (searchResultsNode == null)
                {
                    return -1;
                }

                HtmlNodeCollection list = searchResultsNode.SelectNodes("//a[@class='comp mntl-card-list-items mntl-document-card mntl-card card card--no-image']");

                //Search didn't find anything!
                if (list == null || list.Count == 0)
                {
                    return -1;
                }

                listModel.URLLists = new RecipeURLLists();

                string firstStr;
                string secondStr;

                //skip the first it could be an article
                for (int itemCount = 0; itemCount < list.Count; itemCount++)
                {
                    firstStr = list[itemCount].OuterHtml;
                    if (firstStr.Contains("https://www.foodandwine.com/recipes/"))
                    {
                        secondStr = firstStr.Substring(firstStr.IndexOf("https:"));
                        firstStr = secondStr.Substring(0, secondStr.IndexOf(" ") -1);
                        listModel.URLLists.Add(new Uri(firstStr));
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
        public static RecipeRecordModel ProcessFoodAndWineRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {

            List<string> ingredients = FillIngredientListFoodAndWineForRecipeEntry(doc, 50);
            List<string> directions = FillDirectionsListAllRecipesForRecipeEntry(doc, 30);
            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);

            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='comp type--lion article-heading mntl-text-block']", doc));
            //recipeModel.Website = "FoodAndWine";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//h2[@id='article-subheading_1-0']", doc));
            //recipeBlurbModel.TotalTime = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='recipe-meta-item-body']", doc));
            recipeModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//a[@class='mntl-attribution__item-name']", doc));
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;

            return recipeModel;
        }

        private static List<string> FillIngredientListFoodAndWineForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//div[@id='article-content_1-0']");

            try
            {
                //string section_header = "Ingredients";
                //ingredients.Add(headerTag + section_header);
                HtmlNode htmlNode = ingred_node.SelectSingleNode("//ul[@class='mntl-structured-ingredients__list']");
                HtmlNodeCollection htmlNodes = htmlNode.SelectNodes("//li[@class='mntl-structured-ingredients__list-item ']");
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

        private static List<string> FillDirectionsListAllRecipesForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();

            HtmlNode direct_node = doc.DocumentNode.SelectSingleNode("//div[@class='comp recipe__steps-content mntl-sc-page mntl-block']");

            try
            {
                HtmlNodeCollection htmlNodes = direct_node.SelectNodes("//p[@class='comp mntl-sc-block mntl-sc-block-html']");

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
