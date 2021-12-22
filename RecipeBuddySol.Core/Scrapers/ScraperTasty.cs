using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class ScraperTasty
    {
        private static readonly ScraperTasty instance = new ScraperTasty();

        static ScraperTasty()
        { }

        private ScraperTasty()
        { }

        public static ScraperTasty Instance
        {
            get { return instance; }
        }


        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromTastySearch(string strSearch, RecipeListModel listModel)
        {
            string strQuery = "https://www.tasty.co/search?q=";
            var web = new HtmlWeb();

            string[] myQueryArray = strSearch.Split(' ');

            foreach (var item in myQueryArray)
            {
                strQuery += item + "%20";
            }

            strQuery = strQuery.Remove(strQuery.Length - 3);
            strQuery = strQuery + "&sort=popular";

            try
            {

                var doc = web.Load(strQuery);
                HtmlNode searchResultsNode = doc.DocumentNode.SelectSingleNode("//div[@class='feed__container']");
                
                //Search didn't find anything!
                if (searchResultsNode == null)
                {
                    return -1;
                }


                HtmlNode searchResultsNode1 = searchResultsNode.ChildNodes[0].ChildNodes[0];
                HtmlNodeCollection list = searchResultsNode1.ChildNodes;
                //Search didn't find anything!
                if (list == null || list.Count == 0)
                {
                    return -1;
                }

                //we need to zero out all our lists.
                listModel.URLLists.ClearLists();

                string firstStr;
                string secondStr;
                int i;

                //skip the first it could be an article
                for (int itemCount = 0; itemCount < list.Count; itemCount++)
                {
                    i = list[itemCount].OuterHtml.IndexOf("href");
                    firstStr = list[itemCount].OuterHtml.Substring(i + 7);
                    secondStr = "https://www.tasty.co/" + firstStr.Substring(0, firstStr.IndexOf('\"'));

                    if (listModel.URLLists.Add(new Uri(secondStr)) == -1)
                    {
                        return 0;
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
        public static RecipeRecordModel ProcessTastyRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {
            List<string> ingredients = FillIngredientListTastyForRecipeEntry(doc, 50);
            //no ingredients it isn't a real recipe so we bail
            if (ingredients.Count == 0)
                return null;
            List<string> directions = new List<string>();
            //directions.Add("-Direction");

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);

            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='recipe-name extra-bold xs-mb05 md-mb1']", doc));
            recipeModel.Website = "Tasty";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//p[@class='description xs-text-4 md-text-3 lg-text-2 xs-mb2 lg-mb2 lg-pb05']", doc));
            //recipeBlurbModel.TotalTime = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//p[@class='xs-text-5 extra-bold']", doc));
            recipeModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='byline extra-bold xs-text-4 md-text-2']", doc));
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;

            return recipeModel;
        }

        private static List<string> FillIngredientListTastyForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";

            HtmlNode ingred_node = doc.DocumentNode.SelectSingleNode("//div[@class='ingredients__section xs-mt1 xs-mb3']");

            try
            {
                //string section_header = "Ingredients";
                //ingredients.Add(headerTag + section_header);
                HtmlNodeCollection htmlNodes = ingred_node.SelectNodes("//li[@class='ingredient xs-mb1 xs-mt0']");

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

        private static List<string> FillDirectionListTastyForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            string headerTag = "-";

            HtmlNode directions_top_level = doc.DocumentNode.SelectSingleNode("//ol[@class='prep-steps list-unstyled xs-text-3']");
            if (directions_top_level != null)
            {
                try
                {
                    //directions.Add(headerTag + "Directions");
                    List<HtmlNode> groupslist = directions_top_level.SelectNodes("//li[@class='xs-mb2']").ToList<HtmlNode>();

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
