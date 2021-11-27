using HtmlAgilityPack;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
        public static int GenerateURLsListFromEpicuriousSearch(string strSearch, RecipeBlurbListModel listModel)
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
                ////we need to zero out all our lists.
                //listModel.URLLists.ClearLists();

                foreach (var item in list)
                {
                    string str = item.InnerHtml.Substring(item.InnerHtml.IndexOf('/')).Split('\"')[0];
                    if (listModel.URLLists.Add("https://www.epicurious.com" + str) == -1)
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
        public static RecipeBlurbModel ProcessEpicuriousRecipeType(HtmlDocument doc, char[] splitter, string uri)
        {

            //List<string> ingredients = FillIngredientListEpicuriousForRecipeEntry(doc, 50);
            ////no ingredients it isn't a real recipe so we bail
            //if (ingredients == null || ingredients.Count == 0)
            //    return null;

            //List<string> directions = FillDirectionListEpicuriousForRecipeEntry(doc, 30);

            //RecipeCardModel recipeCardModel = new RecipeCardModel(ingredients, directions);
            RecipeBlurbModel recipeBlurbModel = new RecipeBlurbModel();

            recipeBlurbModel.Title = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML(".//h1[@data-testid='ContentHeaderHed']", doc));
            //HtmlNode timenode = doc.DocumentNode.SelectSingleNode("//ul[@class='InfoSliceList-eUasUM uVRyc']");
            //{
            //    if (timenode != null && timenode.ChildNodes[1] != null)
            //    {
            //        recipeCardModel.TotalTime = timenode.ChildNodes[1].InnerText.Substring(timenode.ChildNodes[1].InnerText.IndexOf("Total Time") + 10);
            //    }
            //}

            recipeBlurbModel.Website = "Epicurious";
            recipeBlurbModel.Description = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//div[@class='container--body-inner']", doc));
            recipeBlurbModel.Author = StringManipulationHelper.CleanHTMLTags(Scraper.FillDataFromHTML("//span[@data-testid='BylineName']", doc));
            recipeBlurbModel.Link = uri;
            recipeBlurbModel.Recipe_Type = Scraper.FillTypeForRecipeEntry(recipeBlurbModel.Title);
            return recipeBlurbModel;
        }

        private static List<string> FillIngredientListEpicuriousForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> ingredients = new List<string>();
            string headerTag = "-";
            try
            {
                HtmlNode ingred_top_level = doc.DocumentNode.SelectSingleNode("//div[@data-testid='IngredientList']");
                if (ingred_top_level == null)
                    return null;
                try 
                {

                    HtmlNodeCollection groupslistHeaders = ingred_top_level.ChildNodes[2].ChildNodes;
                    ingredients.Add(headerTag + "Ingredients");
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
                    ingredients.Add(headerTag + "Ingredients");
                    FillIngredientsSub(ingred_top_level, ingredients, headerTag);
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

        private static List<string> FillDirectionListEpicuriousForRecipeEntry(HtmlDocument doc, int countList)
        {
            List<string> directions = new List<string>();
            string headerTag = "-";
            string s1;
            string s2;
            string s3;

            HtmlNodeCollection groupslist = doc.DocumentNode.SelectSingleNode("//div[@class='BaseWrap-sc-TURhJ InstructionGroupWrapper-hlSqax fNfucB']").ChildNodes;

            if (groupslist != null)
            {
                string section_header = "Directions";
                directions.Add(headerTag + section_header);

                try
                {
                    for (int i = 0; i < groupslist.Count; i++)
                    {
                        if (i == countList - 1)
                        {
                            directions.Add("! List Contains Too Many items, see website !");
                            return directions;
                        }

                        HtmlNode section_node = groupslist[i];
                        if (section_node.FirstChild.NextSibling != null)
                        {
                            s1 = section_node.FirstChild.NextSibling.OuterHtml;
                            int index = s1.IndexOf("<p>") + 3;
                            s2 = s1.Substring(index);
                            s3 = StringManipulationHelper.CleanHTMLTags(s2.Substring(0, s2.IndexOf("</p>")));
                            //sometimes we have a second header burried then we need to dig it out and make another entry
                            if (s3.Contains("<strong>") == true)
                            {
                                s2 = s3.Substring(0, s3.IndexOf("<strong>")).Trim();
                                s1 = s3.Substring(s3.IndexOf("<strong>" + 8), s3.IndexOf("</strong>")).Trim();
                                directions.Add(s2);
                                directions.Add(s1);
                                s2 = s3.Substring(s3.IndexOf("</strong>" + 9)).Trim();
                                directions.Add(s2);
                                i = i + 2;
                            }
                            else
                            {
                                directions.Add(s3);
                            }
                        }
                    }
                }
                catch (Exception e)
                { }
            }

            return Scraper.TrimListToSpecifiedEntries(30, directions);
        }
    }
}
