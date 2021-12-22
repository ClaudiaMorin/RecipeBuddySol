using HtmlAgilityPack;
using RecipeBuddy.Core;
using RecipeBuddy.Core.Models;
using RecipeBuddy.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class ScraperSouthernLiving
    {

        private static readonly ScraperSouthernLiving instance = new ScraperSouthernLiving();

        static ScraperSouthernLiving()
        { }
        private ScraperSouthernLiving()
        { }

        public static ScraperSouthernLiving Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromSouthernLivingSearch(string strSearch, RecipeListModel listModel)
        {
            string strQuery = "https://www.southernliving.com/search?q=";
            var web = new HtmlWeb();

            string[] myQueryArray = strSearch.Split(' ');

            foreach (var item in myQueryArray)
            {
                strQuery += item + '+';
            }
            strQuery = strQuery.TrimEnd('+');

            try
            {
                var doc = web.Load(strQuery);
                HtmlNode searchResultsNode = doc.DocumentNode.SelectSingleNode("//div[@class='search-results-content-container']");
                //Search didn't find anything!
                if (searchResultsNode == null)
                {
                    return -1;
                }

                HtmlNodeCollection list = searchResultsNode.SelectNodes("//a[@class='search-result-title-link elementFont__subhead elementFont__subheadLinkOnly--innerHover']");
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

                for (int itemCount = 0; itemCount < list.Count; itemCount++)
                {
                    //weeds out the non-recipes
                    i = list[itemCount].OuterHtml.IndexOf("www.southernliving.com/recipes/");
                    if (i == -1)
                    i = list[itemCount].OuterHtml.IndexOf("www.southernliving.com/syndication/");

                    if (i != -1)
                    {
                        firstStr = list[itemCount].OuterHtml.Substring(i);
                        secondStr = firstStr.Substring(0, firstStr.IndexOf('"'));

                        if (listModel.URLLists.Add(new Uri("https://" + secondStr)) == -1)
                        {
                            return 0;
                        }
                    }
                }
                return 0;
            }

            catch (Exception e)
            {
                return -1;
            }
        }


        private static List<string> FillIngredientListSouthernLivingForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";

            try
            {
                //This site sticks articles in with the search results which mess everything up.
                HtmlNode ingred_top_level = doc.DocumentNode.SelectSingleNode("//ul[@class='ingredients-section']");
                

                if (ingred_top_level == null)
                {
                    ingredients = null;
                    return ingredients;
                }
                else
                {
                    List<HtmlNode> groupslist = ingred_top_level.SelectNodes("//li[@class='ingredients-item']").ToList<HtmlNode>();
                    //ingredients.Add(headerTag + "Ingredients");

                    foreach (HtmlNode node in groupslist)
                    {
                        ingredients.Add(StringManipulationHelper.CleanHTMLTags(node.InnerText));
                    }
                }
            }
            //something is wrong with the recipe, maybe it is not a recipe at all - bail!
            catch (Exception e)
            {
                return null;
            }

            return Scraper.TrimListToSpecifiedEntries(50, ingredients);
        }


        //private static List<string> FillDirectionListSouthernLivingForRecipeEntry(HtmlDocument doc, int countList)
        //{
        //    List<string> directions = new List<string>();
        //    string headerTag = "-";
        //    try
        //    {
        //        HtmlNode directions_sub_level = doc.DocumentNode.SelectSingleNode("//ul[@class='instructions-section']");
        //        string innerText = directions_sub_level.InnerText.Replace('\n', ' ').Replace("Advertisement", "");
        //        List<string> preDirectionsList = new List<string>();
        //        string subString;

        //        directions.Add(headerTag + "Directions");

        //        while (innerText.TrimStart().Length > 1)
        //        {
        //            innerText = innerText.TrimStart();
        //            subString = innerText.Substring(0, innerText.IndexOf("  "));
        //            preDirectionsList.Add(StringManipulationHelper.CleanHTMLTags(subString));
        //            innerText = innerText.Remove(0, subString.Length);
        //        }

        //        for (int count = 0; count < preDirectionsList.Count;)
        //        {
        //            directions.Add(preDirectionsList[count] + ":  " + preDirectionsList[count + 1]);
        //            count = count + 2;
        //        }
        //    }

        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //    return Scraper.TrimListToSpecifiedEntries(30, directions);
        //}

        /// <summary>
        /// Processes the xml following the SouthernLiving website's specific tweeks that are nessesary to pull data
        /// </summary>
        /// <param name="recipeEntry">The entry we are stuffing this into</param>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="splitter">The the way to split up the description so we only keep two sentences</param>
        /// <param name="uri">website</param>
        public static RecipeRecordModel ProcessSouthernLivingRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {

            List<string> ingredients = FillIngredientListSouthernLivingForRecipeEntry(doc, 50);
            //no ingredients it isn't a real recipe so we bail
            if (ingredients == null)
                return null;
            if (ingredients.Count < 1)
                return null;

            List<string> directions = new List<string>();
            //directions.Add("-Direction");

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);


            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@class='headline heading-content elementFont__display']", doc));
            recipeModel.Website = "SouthernLiving";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='recipe-summary elementFont__dek--paragraphWithin elementFont__dek--linkWithin']", doc));

            //recipeBlurbModel.TotalTime = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//div[@class='recipe-meta-item-body']", doc));

            recipeModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//span[@class='author-name authorName']", doc));

            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;

            return recipeModel;
        }
    }
}
