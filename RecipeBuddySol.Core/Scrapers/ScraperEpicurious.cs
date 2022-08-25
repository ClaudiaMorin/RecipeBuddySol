using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RecipeBuddy.Core.Scrapers
{
    /// <summary>
    /// The Epicurious specific information that is needed to scrap from the websites XML
    /// </summary>
    public sealed class ScraperEpicurious
    {
        private static readonly ScraperEpicurious instance = new ScraperEpicurious();
        static ScraperEpicurious()
        { }
        private ScraperEpicurious()
        { }

        public static ScraperEpicurious Instance
        {
            get { return instance; }
        }


        /// <summary>
        /// Using a search string that is inputted by the user we generate the recipe text for the three pannels.
        /// </summary>
        /// <param name="strSearch"></param>
        public static int GenerateURLsListFromEpicuriousSearch(string strSearch, RecipeListModel listModel)
        {
            List<string> myQuery = new List<string>();

            string strQuery = "https://www.epicurious.com";
            var web = new HtmlWeb();

            myQuery.Add("search");
            myQuery.Add(strSearch);

            foreach (var item in myQuery)
            {
                strQuery += '/' + item;
            }
            strQuery += "?content=recipe";

            try
            {
                var doc = web.Load(strQuery);
                HtmlNodeCollection list = doc.DocumentNode.SelectNodes("//h4[@class='hed']");
                //Search didn't find anything!
                if (list == null || list.Count == 0)
                {
                    return -1;
                }
                //we need to zero out all our lists.
                listModel.URLLists = new RecipeURLLists();

                foreach (var item in list)
                {
                    string str = item.InnerHtml.Substring(item.InnerHtml.IndexOf('/')).Split('\"')[0];
                    if (listModel.URLLists.Add(new Uri("https://www.epicurious.com" + str)) == -1)
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
        /// Processes the xml following the Epicurious website's specific tweeks that are nessesary to pull data
        /// </summary>
        /// <param name="recipeEntry">The entry we are stuffing this into</param>
        /// <param name="doc">HtmlDocument that has the webiste loaded</param>
        /// <param name="splitter">The the way to split up the description so we only keep two sentences</param>
        /// <param name="uri">website</param>
        public static RecipeRecordModel ProcessEpicuriousRecipeType(HtmlDocument doc, char[] splitter, Uri uri)
        {

            List<string> ingredients = FillIngredientListEpicuriousForRecipeEntry(doc, 50);
            List<string> directions = FillDirectionsListAllRecipesForRecipeEntry(doc, 30);

            //no ingredients it isn't a real recipe so we bail
            if (ingredients == null || ingredients.Count == 0)
                return null;

            RecipeRecordModel recipeModel = new RecipeRecordModel(ingredients, directions);

            recipeModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@data-testid='ContentHeaderHed']", doc));
            recipeModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='container--body-inner']", doc));
            recipeModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//span[@data-testid='BylineName']", doc));
            recipeModel.Link = uri.ToString();
            recipeModel.TypeAsInt = (int)Scraper.FillTypeForRecipeEntry(recipeModel.Title);
            recipeModel.ListOfIngredientStrings = ingredients;
            recipeModel.ListOfDirectionStrings = directions;

            return recipeModel;
        }

        private static List<string> FillIngredientListEpicuriousForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            try
            {
                HtmlNode ingred_top_level = doc.DocumentNode.SelectSingleNode("//div[@data-testid='IngredientList']");
                if (ingred_top_level == null)
                    return null;
                try 
                {
                    HtmlNodeCollection groupslistHeaders = ingred_top_level.ChildNodes[2].ChildNodes;
                    if (groupslistHeaders.Count > 0)
                    {
                        foreach (HtmlNode sectionHeader_node in groupslistHeaders)
                        {
                            ingredients.Add(StringManipulationHelper.CleanHTMLTags(sectionHeader_node.InnerText.ToString()));
                        }
                    }
                }
                catch (Exception e) //no subheaders
                {
                    FillIngredientsSub(ingred_top_level, ingredients, "-");
                }
            }
            catch (Exception e)
            { }

            return Scraper.TrimListToSpecifiedEntries(50, ingredients);
        }

        private static void FillIngredientsSub(HtmlNode sectionHeader_node, List<string> ingredients, string headerTag)
        {
            List<HtmlNode> groupslistAmount = sectionHeader_node.SelectNodes("//p[@class='BaseWrap-sc-TwdDQ BaseText-fFHxRE Amount-Wcygw hlNbBe dZgHQP jpdXhZ']").ToList<HtmlNode>();
            List<HtmlNode> groupslistIngred = sectionHeader_node.SelectNodes("//div[@class='BaseWrap-sc-TwdDQ BaseText-fFHxRE Description-dSowHq hlNbBe dZgHQP eRguAM']").ToList<HtmlNode>();
            int count = 0;
            string ingred;
            string amount;

            foreach (HtmlNode node in groupslistAmount)
            {
                amount = StringManipulationHelper.CleanHTMLTags(node.InnerText);
                ingred = StringManipulationHelper.CleanHTMLTags(groupslistIngred[count].InnerText);
                ingredients.Add(amount + " " + ingred);
                count++;
            }
        }

        private static List<string> FillDirectionsListAllRecipesForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();

            HtmlNode direct_node = doc.DocumentNode.SelectSingleNode("//div[@class='InstructionGroupWrapper-hmyafp bJfiL']");

            try
            {
                HtmlNodeCollection htmlNodes = direct_node.SelectNodes("//div[@class='BaseWrap-sc-UABmB BaseText-fETRLB InstructionBody-huDCkh hkSZSE xURKj eyjXXE']");

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
