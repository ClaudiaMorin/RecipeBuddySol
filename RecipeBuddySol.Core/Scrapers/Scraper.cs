using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using RecipeBuddy.Core;
using RecipeBuddy.Core.Helpers;
using RecipeBuddy.Core.Models;

namespace RecipeBuddy.Core.Scrapers
{
    public enum Type_of_Websource { None, AllRecipes, SouthernLiving, FoodAndWine }

    public sealed class Scraper
    {
            
        private static readonly Scraper instance = new Scraper();

        static Scraper()
        { }

        private Scraper()
        { }

        public static Scraper Instance
        {
            get { return instance; }
        }



        /// <summary>
        /// Either fill the node if the data can be found converting some of the "special" HTML characters, trimming white spaces, etc
        /// will return a blank string if the target node can't be found in the HTML.
        /// </summary>
        public static string FillDataFromHTML(string target, HtmlDocument doc)
        {
            string myProp = "";

            try
            {
                if (doc.DocumentNode.SelectSingleNode(target) == null)
                    return "";

                myProp = doc.DocumentNode.SelectSingleNode(target).InnerText.Trim().Replace("&#8211;", "-").Replace("&quot;", "\"").Replace("&#8212;", "-").Replace("&#39;", "'").Replace("&#x27;", "'");
            }
            catch (Exception e)
            { }
            return myProp;
        }

        /// <summary>
        /// Fills the data we are looking for based on the target string
        /// and returns a cleaned version of the innertext
        /// </summary>
        /// <param name="target">the string we are using to identify the node</param>
        /// <param name="node">the top node to use to begin the search</param>
        /// <returns></returns>
        public static string FillDataFromHTML(string target, HtmlNode node)
        {
            string myProp = "";
            
            try
            {
                if (node == null || node.SelectSingleNode(target) == null)
                    return "";

                myProp = node.SelectSingleNode(target).InnerText.Trim().Replace("&#8211;", "-").Replace("&quot;", "\"").Replace("&#8212;", "-").Replace("&#39;", "'").Replace("&#x27;", "'");
            }
            catch (Exception e)
            { }
            return myProp;
        }

        /// <summary>
        /// First Pass on taking data from the HTML page and sorting it into buckets
        /// the ingredients and directions bucket are HMLCollections so that I can keep the items seperated
        /// </summary>
        /// <returns>A filled RecipeCard</returns>
        public static RecipeRecordModel ScrapeDataForRecipeEntry(Uri uri)
        {
            //Bad uri string -- Bail.
            if (uri == null)
                return null;
            char[] splitter = { '.', '?', '!', '\n' };

            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(uri);
               

                if (uri.Host == "www.allrecipes.com")
                {
                    RecipeRecordModel dataBlurbAllRecipes = Scraper_AllRecipes_Southern_FoodAndWine.ProcessRecipeType(doc, splitter, uri);
                        return dataBlurbAllRecipes;
                }


                if (uri.Host == "www.southernliving.com")
                {
                    RecipeRecordModel dataSouthernLiving = Scraper_AllRecipes_Southern_FoodAndWine.ProcessRecipeType(doc, splitter, uri);
                        return dataSouthernLiving;
                }

                if (uri.Host == "www.foodandwine.com")
                {
                    RecipeRecordModel dataFoodAndWine = Scraper_AllRecipes_Southern_FoodAndWine.ProcessRecipeType(doc, splitter, uri);
                        return dataFoodAndWine;
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }

        /// <summary>
        /// Fills the type for the recipe so that we can add it to the entry
        /// </summary>
        /// <param name="entry"></param>
        public static Type_Of_Recipe FillTypeForRecipeEntry(string Title)
        {
            Type_Of_Recipe type = Type_Of_Recipe.Unknown;

            if (Title.ToLower().Contains("cake") || Title.ToLower().Contains("brownie"))
            {
                type = Type_Of_Recipe.Cake;
            }

            if (Title.ToLower().Contains("pie") || Title.ToLower().Contains("pastery") || Title.ToLower().Contains("doughnut"))
            {
                type = Type_Of_Recipe.Pastry;
            }

            if (Title.ToLower().Contains("cookie") || Title.ToLower().Contains("bar"))
            {
                type = Type_Of_Recipe.Cookie;
            }

            if (Title.ToLower().Contains("custard") || Title.ToLower().Contains("pudding") || Title.ToLower().Contains("flan"))
            {
                type = Type_Of_Recipe.Custard;
            }

            if (Title.ToLower().Contains("soup") || Title.ToLower().Contains("stew") || Title.ToLower().Contains("chili"))
            {
                type = Type_Of_Recipe.SoupStew;
            }

            if (Title.ToLower().Contains("lamb"))
            {
                type = Type_Of_Recipe.Lamb;
            }

            if (Title.ToLower().Contains("pork"))
            {
                type = Type_Of_Recipe.Pork;
            }

            if (Title.ToLower().Contains("chicken") || Title.ToLower().Contains("duck") || 
                Title.ToLower().Contains("hen") || Title.ToLower().Contains("goose") || Title.ToLower().Contains("turkey"))
            {
                type = Type_Of_Recipe.Poultry;
            }

            if (Title.ToLower().Contains("fish") || Title.ToLower().Contains("clam") || Title.ToLower().Contains("shrimp") || Title.ToLower().Contains("halibut") || Title.ToLower().Contains("lobster")
            || Title.ToLower().Contains("mussel") || Title.ToLower().Contains("salmon") || Title.ToLower().Contains("cod") || Title.ToLower().Contains("trout"))
            {
                type = Type_Of_Recipe.Seafood;
            }

            if (Title.ToLower().Contains("beef"))
            {
                type = Type_Of_Recipe.Beef;
            }

            if (Title.ToLower().Contains("tofu") ||Title.ToLower().Contains("tempeh") || Title.ToLower().Contains("vegan") || Title.ToLower().Contains("vegetarian"))
            {
                type = Type_Of_Recipe.Tofu;
            }

            if (Title.ToLower().Contains("bread") || Title.ToLower().Contains("roll") || Title.ToLower().Contains("croissant") || Title.ToLower().Contains("cracker"))
            {
                type = Type_Of_Recipe.Bread;
            }

            if (Title.ToLower().Contains("salad"))
            {
                type = Type_Of_Recipe.Salad;
            }

            if (Title.ToLower().Contains("bean") ||Title.ToLower().Contains("casserole") || Title.ToLower().Contains("vegetables") || Title.ToLower().Contains("cabbage") )
            {
                type = Type_Of_Recipe.SideDish;
            }

            if (Title.ToLower().Contains("appetizer") )
            {
                type = Type_Of_Recipe.Appetizer;
            }

            if (Title.ToLower().Contains("souffle") || Title.ToLower().Contains("egg") || Title.ToLower().Contains("quiche"))
            {
                type = Type_Of_Recipe.Eggs;
            }

            if (Title.ToLower().Contains("cheese") || Title.ToLower().Contains("yougert"))
            {
                type = Type_Of_Recipe.Dairy;
            }

            if (Title.ToLower().Contains("candy") || Title.ToLower().Contains("fudge") || Title.ToLower().Contains("toffee"))
            {
                type = Type_Of_Recipe.Candy;
            }

            return type;
        }

        /// <summary>
        /// Verifies that the list is under the number allowed or trims it down
        /// </summary>
        /// <param name="numberOfEntriesAllowed">The number of entries allow in the list</param>
        /// <param name="list">The list of entries we are dealing with, directions or ingredients</param>
        /// <returns></returns>
        public static List<string> TrimListToSpecifiedEntries(int numberOfEntriesAllowed, List<string> list)
        {
            //Trim list down to only X number of entries before returning it
            //Check to see if the amount of ingredients is over our limit, if not just return!
            if (list.Count < numberOfEntriesAllowed - 1)
            {
                return list;
            }
            else //over the limit so we need to cut the list down!
            {
                int count = list.Count - 1;

                for (; count > numberOfEntriesAllowed - 2; count--)
                {
                    list.RemoveAt(count);
                }

                list.Add("! List Contains Too Many items, see website !");;
            }

            return list;
        
        }

        ///// <summary>
        ///// Generic Recipe Processing 
        ///// </summary>
        ///// <param name="UploadedText">The textfile uploaded by the user</param>
        ///// <returns>A filled in object of type RecipeCardModel</returns>
        //public static RecipeDisplayModel ProcessUploatedRecipe(string uploadedText)
        //{
        //    string title = uploadedText.Substring(0, uploadedText.IndexOf("\r\n"));
        //    string recipeStr = uploadedText.Substring(title.Length);

        //    int indexIngred = uploadedText.IndexOf("Ingredients");
        //    if (indexIngred == -1)
        //    {
        //        return new RecipeDisplayModel();
        //    }

        //    List<string> ingredList = new List<string>();
        //    ingredList.Add("-Ingredients");

        //    string ingredStr = uploadedText.Substring(indexIngred).Trim();
        //    ingredStr = ingredStr.Substring(ingredStr.IndexOf("\r\n")).Trim();
        //    string directStr = ingredStr.Substring(ProcessUploatedRecipeIngredients(ingredStr, ingredList)).Trim();

        //    List<string> directList = new List<string>();
        //    directList.Add("-Directions");

        //    ProcessUploatedRecipeDirections(directStr, directList);

        //    RecipeDisplayModel recipeCardModel = new RecipeDisplayModel(ingredList, directList);
        //    recipeCardModel.Title = title;

        //    return recipeCardModel;
        //}

        /// <summary>
        /// Processes all of the ingredients found in a recipe and adds them to the list passed to the recipecardmodel's constructor
        /// </summary>
        /// <param name="uploadedText">The section of text pulled from the recipe that comes right after the word "ingredients"</param>
        /// <param name="listOfIngredients">The list of ingredient items that will be passed to the recipecardmodel</param>
        /// <returns>int indicating where to start the directions processing of the recipe</returns>
        private static int ProcessUploatedRecipeIngredients(string uploadedText, List<string> listOfIngredients)
        {
            int indexEndIngred = FindIngredientsEndIndex(uploadedText);
            string IngredSection = uploadedText.Substring(0, indexEndIngred).Trim();
            int charsToRemove = IngredSection.Length;
            string ingred1;

            while (IngredSection.Length > 0)
            {
                //Start by snipping off a line and the first word.
                if (IngredSection.IndexOf("\r\n") != -1)
                {
                    ingred1 = IngredSection.Substring(0, IngredSection.IndexOf("\r\n"));
                    IngredSection = IngredSection.Substring(ingred1.Length).Trim();
                    ingred1.Trim();
                }
                else //this happens on the last line so just add it.
                {
                    ingred1 = IngredSection.Trim();
                    IngredSection = "";
                    listOfIngredients.Add(ingred1);
                    break;
                }
                
                if (StringManipulationHelper.IsFraction(ingred1.Substring(0, 1)) == true)
                {
                    //Not a multi-line ingredient! Add it!
                    if (StringManipulationHelper.IsFraction(IngredSection.Substring(0, 1)) == true)
                    {
                        listOfIngredients.Add(ingred1);
                    }
                    else //multi-line ingredient type add the ingred1 back to the front and reprocess.
                    {
                        IngredSection = ingred1 + " " + IngredSection;
                    }
                }
            }

            return charsToRemove;
        }

            
  

        /// <summary>
        /// Find the end of the ingredients section: Preheat, Step 1, Directions, Preparation, look for full sentences?     
        /// </summary>
        /// <param name="uploadedText"></param>
        /// <returns></returns>
        private static int FindIngredientsEndIndex(string uploadedText)
        {
            int retVal = -1;
            int temp;

            if (uploadedText.IndexOf("Step") != -1)
            {
                retVal = uploadedText.IndexOf("Step");
            }

            if (uploadedText.IndexOf("Preparation") != -1)
            {
                temp = uploadedText.IndexOf("Preparation");
                if (retVal != -1 && temp < retVal)
                    retVal = temp;
            }

            if (uploadedText.IndexOf("Directions") != -1)
            {
                temp = uploadedText.IndexOf("Directions");
                if (retVal != -1 && temp < retVal)
                    retVal = temp;
            }

            if (uploadedText.IndexOf("Preheat") != -1)
            {
                temp = uploadedText.IndexOf("Preheat");
                if (retVal != -1 && temp < retVal)
                    retVal = temp;
            }

            return retVal;
        }

        private static void ProcessUploatedRecipeDirections(string uploadedText, List<string> listOfIngredients)
        {
            string direct1;
            string directStr = uploadedText;

            //Check if there are "Steps" its a great hack.
            if (directStr.Contains("Step"))
            {
                direct1 = uploadedText.Substring(0, directStr.IndexOf("Step"));
                listOfIngredients.Add(direct1.Replace("\r\n", " ").Trim());
                //This removes Step.
                directStr = directStr.Substring(direct1.Length + 4).Trim();

                while (directStr.Length > 0)
                {
                    if (directStr.Contains("Step"))
                    {
                        direct1 = directStr.Substring(0, directStr.IndexOf("Step"));
                        directStr = directStr.Substring(direct1.Length + 4).Trim();
                        listOfIngredients.Add("Step " + direct1.Replace("\r\n", " ").Trim());
                    }
                    else // we are at the end, put the rest into the last dictionary entry
                    {
                        listOfIngredients.Add("Step " + directStr.Replace("\r\n", " ").Trim());
                        directStr = "";
                        return;
                    }
                }
            }

            //Start by snipping off a line and adding it to Directions.
            direct1 = uploadedText.Substring(0, uploadedText.IndexOf(".\r\n") + 1);
            directStr = uploadedText.Substring(direct1.Length).Trim();
            direct1 = direct1.Replace("\r\n", " ").Trim();

            while (direct1.Length > 0)
            {
                listOfIngredients.Add(direct1);
                direct1 = directStr.Substring(0, directStr.IndexOf(".\r\n") + 1);
                directStr = directStr.Substring(direct1.Length).Trim();
                direct1 = direct1.Replace("\r\n", " ").Trim();
            }
        }
    }
}
