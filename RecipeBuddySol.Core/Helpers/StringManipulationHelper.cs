using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace RecipeBuddy.Core.Helpers
{
    public static class StringManipulationHelper
    {


        /// <summary>
        /// Tests a string to verify that it is a fraction, not a whole number
        /// </summary>
        /// <param name="quantityStr"></param>
        /// <returns></returns>
        public static bool IsFraction(string quantityStr)
        {
            bool breturn = false;
            if(quantityStr.Contains("/"))
                return true;

            for (int i = 0; i < quantityStr.Length; i++)
            {
                switch (quantityStr[i])
                {

                    case '½':
                        return true;

                    case '¼':
                        return true;

                    case '¾':
                        return true;

                    case '⅛':
                        return true;

                    case '⅓':
                        return true;

                    case '⅔':
                        return true;
                }
            }

            

            return breturn;
        }

        /// <summary>
        /// a convertion from vulgar fractions to a decimal representation in string form
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static double ConvertVulgarFaction(string fraction)
        {
            if (fraction.IndexOf('½') != -1 || fraction.IndexOf("1/2") != -1)
            {
                return 0.500;
            }

            if (fraction.IndexOf('¼') != -1 || fraction.IndexOf("1/4") != -1)
            {
                return .250;
            }

            if (fraction.IndexOf('¾') != -1 || fraction.IndexOf("3/4") != -1)
            {
                return .750;
            }

            if (fraction.IndexOf('⅓') != -1 || fraction.IndexOf("1/3") != -1)
            {
                return .3333;
            }

            if (fraction.IndexOf('⅔') != -1 || fraction.IndexOf("2/3") != -1)
            {
                return .6666;
            }

            if (fraction.IndexOf('⅛') != -1 || fraction.IndexOf("1/8") != -1)
            {
                return 0.125;
            }

            return -1;
        }

        /// <summary>
        /// Adds the correct abreveation for the measurement type
        /// </summary>
        /// <param name="measuretype"></param>
        /// <returns></returns>
        public static string CheckMeasureString(string measuretype)
        {
            string typeToCheck = measuretype.Trim().ToLower();
            string toReturn = "";

            switch (typeToCheck)
            {
                case "c":
                case "cup":
                case "cups":
                case "cup(s)":
                    toReturn = "C";
                    break;

                case "tsp":
                case "tsp.":
                case "teaspoon":
                case "teaspoons":
                case "teaspoon(s)":
                    toReturn = "tsp.";
                    break;

                case "tbsp":
                case "tbsp.":
                case "tablespoon":
                case "tablespoons":
                case "tablespoon(s)":
                    toReturn = "Tbsp.";
                    break;

                case "lbs":
                case "lbs.":
                case "pound":
                case "pounds":
                case "pound(s)":
                    toReturn = "lbs.";
                    break;

                case "g":
                case "g.":
                case "gram":
                case "grams":
                case "gram(s)":
                    toReturn = "grams";
                    break;
            }

            return toReturn;
        }

        public static string FinalQuantityString(double quantity, string measureType)
        {

            double quantityASDouble = Math.Round(quantity, 3, MidpointRounding.AwayFromZero);
            int wholeNumberOfInt = (int)quantityASDouble;

            double remainderAsDouble = Math.Round(quantityASDouble - wholeNumberOfInt, 3, MidpointRounding.AwayFromZero);


            string stringForCompare = measureType.ToLower().Trim();
            string retVal = "";

            //Great Starting place for most of the measurement types
            //if not, as with teaspoons we will overwrite these values!
            if (retVal.Length == 0)
            {
                if (wholeNumberOfInt > 0)
                    retVal += wholeNumberOfInt;
                if (remainderAsDouble > 0)
                    retVal += ConvertDoubleToVulgarFaction(remainderAsDouble);
            }

            switch (measureType.ToLower())
            {
                case "g":
                case "g.":
                case "gram":
                case "grams":
                case "gram(s)":
                    retVal += " grams";
                    break;

                case "lbs":
                case "lbs.":
                case "pound":
                case "pounds":
                case "pound(s)":
                    retVal += " lbs";
                    break;

                case "cup":
                case "cups":
                case "c":
                case "c.":
                    retVal += " C";
                    break;

                case "teaspoon":
                case "teaspoons":
                case "tsp":
                case "tsp.":
                    retVal = "";
                    bool tablespoons = false;
                    if (wholeNumberOfInt >= 3)
                    {
                        tablespoons = true;
                        int newTbls = wholeNumberOfInt / 3;
                        retVal += newTbls;
                        wholeNumberOfInt -= newTbls * 3;
                    }
                    if (tablespoons)
                    { 
                        if(remainderAsDouble > 0 || wholeNumberOfInt > 0)
                            retVal += " Tbsp + ";
                        else
                            retVal += " Tbsp";
                    }
                        
                    if (wholeNumberOfInt > 0) //still have some tsp left
                    {
                        if (remainderAsDouble > 0)
                            retVal += wholeNumberOfInt;
                        else
                            retVal += wholeNumberOfInt + " tsp";
                    }

                    if (remainderAsDouble > 0)
                    {
                        retVal += ConvertDoubleToVulgarFaction(remainderAsDouble) + " tsp";
                    }

                    break;


                case "tablespoon":
                case "tablespoons":
                case "tbsp":
                case "tbsp.":
                    retVal = "";
                    bool cups = false;
                    while (wholeNumberOfInt >= 4)
                    {
                        cups = true;
                        if (wholeNumberOfInt >= 16)
                        {
                            int newCups = wholeNumberOfInt / 16;
                            retVal += newCups;
                            wholeNumberOfInt -= newCups * 16;
                        }
                        if (wholeNumberOfInt >= 12)
                        {
                            retVal += "¾";
                            wholeNumberOfInt -= 12;
                        }
                        if (wholeNumberOfInt >= 8)
                        {
                            retVal += "½";
                            wholeNumberOfInt -= 8;
                        }
                        if (wholeNumberOfInt >= 4)
                        {
                            retVal += "¼";
                            wholeNumberOfInt -= 4;
                        }
                    }

                    if (cups) //we managed to simplify to cups
                    {
                        if(wholeNumberOfInt > 0 || remainderAsDouble > 0)
                            retVal += " C + ";
                        else
                            retVal += " C";
                    }
                    //Do we have any remainders?
                    if (wholeNumberOfInt < 4 && wholeNumberOfInt > 0)
                    { 
                        if(remainderAsDouble > 0)
                            retVal += wholeNumberOfInt + ConvertDoubleToVulgarFaction(remainderAsDouble) + " Tbsp";
                        else
                            retVal += wholeNumberOfInt + " Tbsp";
                    }
                    break;

            }
                
            return retVal;
        }


        /// <summary>
        /// a convertion a decimal representation to a vulgar fraction 
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static char ConvertDoubleToVulgarFaction(double doubleToConvert)
        {
            if (doubleToConvert == 0.250)
                return '¼';
            if (doubleToConvert == 0.500)
                return '½';
            if (doubleToConvert == 0.750)
                return '¾';
            if (doubleToConvert == 0.125)
                return '⅛';
            if (doubleToConvert == 0.375)
                return '⅜';
            if (doubleToConvert == 0.625)
                return '⅝';
            if (doubleToConvert == 0.667 || doubleToConvert == 0.666)
                return '⅔';
            if (doubleToConvert == 0.333)
                return '⅓';
            if (doubleToConvert == 0.875)
                return '⅞';


            return '0';
        }

        public static int TryToConvertWritenNumbersToDigits(string amount)
        {
            if (string.Compare(amount.ToLower(), "one") == 0)
                return 1;
            if (string.Compare(amount.ToLower(), "two") == 0)
                return 2;
            if (string.Compare(amount.ToLower(), "three") == 0)
                return 3;
            if (string.Compare(amount.ToLower(), "four") == 0)
                return 4;
            if (string.Compare(amount.ToLower(), "five") == 0)
                return 5;
            if (string.Compare(amount.ToLower(), "six") == 0)
                return 6;
            if (string.Compare(amount.ToLower(), "seven") == 0)
                return 7;
            if (string.Compare(amount.ToLower(), "eight") == 0)
                return 8;
            if (string.Compare(amount.ToLower(), "nine") == 0)
                return 9;
            if (string.Compare(amount.ToLower(), "ten") == 0)
                return 10;
            else
                return 0;
        }

        #region Turning the ingredients and directions into lists to be stored/returned form the DB

        public static string TurnListIntoStringForDB(List<string> listToConvertToString)
        {
            string returnString = "";

            foreach (string s in listToConvertToString)
            {
                returnString = returnString + "|" + s;
            }

            returnString = returnString.Remove(0, 1);

            return returnString;
        }

        public static List<string> TurnStringintoListFromDB(string stringToConvertToList)
        {
            List<string> list = new List<string>();

            char[] stringSplitter = new char[] { '|' };

            string[] substrings = stringToConvertToList.Split(stringSplitter);

            foreach (string s in substrings)
            {
                if (s.Length > 1)
                {
                    list.Add(s);
                }
            }

            return list;
        }

        #endregion

        #region cleaning up the HTML code and trimming

        public static string CleanHTMLTags(string target)
        {
            string returnStr = target.Replace("&#8211;", "-").Replace("&quot;", "\"").Replace("&deg;", "°").Replace("&rsquo;", "'").Replace("&#x27;", "'").Replace("&amp;", "&").Replace("& mdash;", "-").Replace("&ntilde;", "ñ")
               .Replace("&egrave;", "è").Replace("&#39;", "'"); 
            returnStr = returnStr.Trim();
            return returnStr;
        }

        #endregion


    }
}
