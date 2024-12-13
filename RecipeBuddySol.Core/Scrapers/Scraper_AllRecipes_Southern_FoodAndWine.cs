using HtmlAgilityPack;
using System.Xml.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;


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

            strQuery += "+recipe";


                try
                {
                    var doc = web.Load(strQuery);

                    HtmlNode Node1 = doc.DocumentNode.SelectSingleNode("//div[@id='mntl-search-results__content_1-0']");
                    HtmlNodeCollection list = Node1.SelectNodes("//a[@class='comp mntl-card-list-card--extendable mntl-universal-card mntl-document-card mntl-card card card--no-image']");


                    //Search didn't find anything!
                    if (list == null || list.Count == 0)
                    {
                        return -1;
                    }

                    listModel.URLLists = new RecipeURLLists();

                    foreach (HtmlNode node in list)
                    {
                        string s = node.Attributes[4].Value;
                        listModel.URLLists.Add(new Uri(s));      
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
            RecipeRecordModel recipeModel = RecipeRecordModelFactory(doc, uri);
            if (recipeModel == null)
                return null;

            if (recipeModel.Description.Length > 200)
            {
                recipeModel.Description = recipeModel.Description.Substring(0, 200);
                recipeModel.Description = recipeModel.Description.Substring(0, recipeModel.Description.LastIndexOf(' '));
            }

            recipeModel.Author = "By: " + StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//a[@class='mntl-attribution__item-name']", doc));
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            return recipeModel;
        }

        /// <summary>
        /// Basically this handles the differences between the formatting in the different websites
        /// </summary>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static RecipeRecordModel RecipeRecordModelFactory(HtmlDocument doc, Uri uri)
        {
            List<string> ingredients = FillIngredientListRecipeEntry(doc, 50);
            if (ingredients == null)
                return null;

            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;
            

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients);
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(doc.DocumentNode.SelectSingleNode("//p[@class='article-subheading text-body-100']").InnerText);
            HtmlNode nodeForTitle = doc.DocumentNode.SelectSingleNode("//div[@class='comp article-header--recipe mm-recipes-article-header mntl-article-header']");
            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//h1[@class='article-heading text-headline-400']", nodeForTitle));
            recipeModel.ListOfIngredientStrings = ingredients;

            if (uri.Host == "www.southernliving.com")
            {
                recipeModel.ListOfDirectionStrings = FillDirectionsSouthernLiving(doc, 30);
            }
            else
            {
                recipeModel.ListOfDirectionStrings = FillDirectionsFoodAndWineAndAllRecipes(doc, 30);
            }

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

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//ul[@class='mm-recipes-structured-ingredients__list']");

            if (ingred_node == null)
                return null;

            try
            {
                HtmlNodeCollection htmlNodes = ingred_node.SelectNodes("//li[@class='mm-recipes-structured-ingredients__list-item ']");

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

        private static List<string> FillDirectionsFoodAndWineAndAllRecipes(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            try
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='comp mm-recipes-steps__content mntl-sc-page mntl-block ']");
                if (node != null) 
                {
                    HtmlNodeCollection htmlNodes = node.SelectNodes("//p[@class='comp mntl-sc-block mntl-sc-block-html']");
                    for (int i = 0; i < countList; i++)
                    {
                        HtmlNode sectionHeader_node = htmlNodes[i];
                        string tempDirection = StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText);
                        if (tempDirection.Length > 2)
                            directions.Add(tempDirection);
                    }
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(countList, directions);
        }

        private static List<string> FillDirectionsSouthernLiving(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            try
            {

                HtmlNode direct_node = doc.DocumentNode.SelectSingleNode("//ol[@class='comp mntl-sc-block mntl-sc-block-startgroup mntl-sc-block-group--OL']");

                if (direct_node != null)
                {
                    HtmlNodeCollection htmlNodes = direct_node.ChildNodes;
                    for (int i = 0; i < countList; i++)
                    {
                        HtmlNode sectionHeader_node = htmlNodes[i];
                        string tempDirection = StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText);
                        if(tempDirection.Length > 2)
                          directions.Add(tempDirection);
                    }
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(countList, directions);
        }

    }
}
