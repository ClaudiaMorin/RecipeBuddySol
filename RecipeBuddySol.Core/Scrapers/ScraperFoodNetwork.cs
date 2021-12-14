using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RecipeBuddy.Core.Scrapers
{
    public sealed class ScraperFoodNetwork
    {

        private static readonly ScraperFoodNetwork instance = new ScraperFoodNetwork();

        static ScraperFoodNetwork()
        { }
        private ScraperFoodNetwork()
        { }

        public static ScraperFoodNetwork Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromFoodNetworkSearch(string strSearch, RecipeListModel listModel)
        {
            string strQuery = "https://www.foodnetwork.com/search/";
            var web = new HtmlWeb();

            string[] myQueryArray = strSearch.Split(' ');

            foreach (var item in myQueryArray)
            {
                strQuery += item + '-';
            }

            try
            {
                var doc = web.Load(strQuery);
                HtmlNode searchResultsNode = doc.DocumentNode.SelectSingleNode("//section[@class='o-SiteSearchResults']");
                //Search didn't find anything!
                if (searchResultsNode == null)
                {
                    return -1;
                }

                HtmlNodeCollection list = searchResultsNode.SelectNodes("//section[@class='o-RecipeResult o-ResultCard']");
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
                    i = list[itemCount].InnerHtml.IndexOf("www.foodnetwork.com");
                    firstStr = list[itemCount].InnerHtml.Substring(i);
                    secondStr = firstStr.Substring(0, firstStr.IndexOf('"'));

                    if (listModel.URLLists.Add(new Uri("https://" + secondStr)) == -1)
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

        private static List<string> FillDirectionListFoodNetworkForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            string headerTag = "-";
            try 
            {
                HtmlNode mainNode = doc.DocumentNode.SelectSingleNode("//section[@class='o-Method']");
                HtmlNode directions_top_level = mainNode.SelectSingleNode("//div[@class='o-Method__m-Body']");
                if (directions_top_level == null)
                    return null;
                HtmlNodeCollection directions_sub_level = directions_top_level.SelectNodes("//li[@class='o-Method__m-Step']");
                if (directions_sub_level != null)
                {
                    string section_header = "Directions";
                    directions.Add(headerTag + section_header);

                    List<HtmlNode> groupslist = directions_sub_level.ToList<HtmlNode>();
                    string innerText1;

                    //get rid of the nodes that aren't useful.
                    foreach (HtmlNode node in groupslist)
                    {
                        innerText1 = node.InnerText.Replace('\n', ' ').Trim();
                        directions.Add(StringManipulationHelper.CleanHTMLTags(innerText1));
                    }
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(30, directions);
        }

        private static List<string> FillIngredientListFoodNetworkForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";
            try
            {
                HtmlNode ingred_top_level1 = doc.DocumentNode.SelectSingleNode("//section[@class='o-Recipe']");
                HtmlNode ingred_top_level2 = ingred_top_level1.SelectSingleNode("//section[@class='o-Ingredients']");
                if (ingred_top_level2 == null)
                    return null;

                HtmlNode ingred_top_level3 = ingred_top_level2.SelectSingleNode("//div[@class='o-Ingredients__m-Body']");
                if (ingred_top_level3 == null)
                    return null;

                List<HtmlNode> groupslist = ingred_top_level3.ChildNodes.ToList<HtmlNode>();
                List<HtmlNode> groupslistProcessed = new List<HtmlNode>();

                //get rid of the nodes that aren't useful.
                foreach (HtmlNode node in groupslist)
                {
                    if (node.OuterHtml.Contains("o-Ingredients__a-SubHeadline") || node.OuterHtml.Contains("o-Ingredients__a-Ingredient\">"))
                    {
                        groupslistProcessed.Add(node);
                    }
                }

                string innerText1;
                string innerText2;
                bool firstIngred = true;


                if (groupslistProcessed.Count > 1)
                {
                    foreach (HtmlNode node in groupslistProcessed)
                    {

                        //test to see if we have a section header... if not we need to fake it 
                        if (node.OuterHtml.Contains("o-Ingredients__a-SubHeadline") && firstIngred == true)
                        {
                            innerText1 = node.InnerText.Replace('\n', ' ');
                            innerText2 = innerText1.Substring(0, innerText1.IndexOf(':') + 1).Trim();
                            ingredients.Add(headerTag + innerText2);
                            firstIngred = false;
                        }
                        else if (firstIngred == true)
                        {
                            ingredients.Add(headerTag + "Ingredients");
                            firstIngred = false;
                        }
                        else
                        {
                            innerText1 = node.InnerText.Replace('\n', ' ').Trim();
                            ingredients.Add(StringManipulationHelper.CleanHTMLTags(innerText1));
                        }
                    }
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(50, ingredients);
        }

        /// <summary>
        /// Processes the xml following the Epicurious website's specific tweeks that are nessesary to pull data
        /// </summary>
        /// <param name="recipeEntry">The entry we are stuffing this into</param>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="splitter">The the way to split up the description so we only keep two sentences</param>
        /// <param name="uri">website</param>
        public static RecipeRecordModel ProcessFoodNetworkRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {

            List<string> ingredients = FillIngredientListFoodNetworkForRecipeEntry(doc, 50);
            //no ingredients it isn't a real recipe so we bail
            if (ingredients == null || ingredients.Count == 0)
                return null;

            List<string> directions = new List<string>();
            directions.Add("-Direction");

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);

            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//span[@class='o-AssetTitle__a-HeadlineText']", doc));
            recipeModel.Website = "FoodNetwork";
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='o-AssetDescription__a-Description']", doc));

/*            recipeBlurbModel.TotalTime = StringManipulationHelper.CleanHTMLTags(GetTotalTime(doc))*/;
            HtmlNode ingred_top_level = doc.DocumentNode.SelectSingleNode("//span[@class='o-Attribution__a-Name']");
            if (ingred_top_level != null)
            {
                string str1 = ingred_top_level.InnerText.Replace('\n', ' ').Trim();
                recipeModel.Author = StringManipulationHelper.CleanHTMLTags(str1.Substring(str1.IndexOf("of") + 3));
            }
            else
                recipeModel.Author = "";

            //recipeCardModel.Date = "";
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;

            return recipeModel;
        }

        /// <summary>
        /// Gets the total time but is nested in a try/catch so if the span we are looking for doesn't exist it won't crash
        /// </summary>
        /// <param name="doc">HTMLDocument that we are searching</param>
        /// <returns>string for the total time</returns>
        public static string GetTotalTime(HtmlDocument doc)
        {
            try
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//span[@class='o-RecipeInfo__a-Description m-RecipeInfo__a-Description--Total']");
                if (node != null)
                { return node.InnerText; }

                return ""; 
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
