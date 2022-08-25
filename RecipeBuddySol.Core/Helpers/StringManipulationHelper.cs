using System;
using System.Collections.Generic;
using System.Linq;


namespace RecipeBuddy.Core.Helpers
{
    public static class StringManipulationHelper
    {
        #region Convertion to and from Float-String-VulgarFraction
        /// <summary>
        /// converts the "vulgar fraction into a float so it can be used mathmatically
        /// </summary>
        /// <param name="target">the string we are searching for the fraction to convert</param>
        /// <returns>the float that result from the convertion</returns>
        public static float ConvertVulgerFractionStringToFloat(string target)
        {
            string amount = " ";

            if (target.Contains(" "))
            {
                amount = target.Substring(0, target.IndexOf(' '));
            }
            else
            {
                amount = target;
            }

            if (amount.Contains('-')) //to handle the case where the amounts are 3-4 apples... 
                amount = amount.Substring(0, target.IndexOf('-'));

            string convertedfraction = ConvertVulgarFaction(amount);

            if (string.Compare(convertedfraction, "-1") != 0)
            {
                float parseFloat;
                //parse to a float and return
                if (float.TryParse(convertedfraction, out parseFloat) == true)
                {
                    return parseFloat;
                }     
            }

            return -1;
        }

        /// <summary>
        /// Tests a string to verify that it is a number.
        /// </summary>
        /// <param name="quantityStr"></param>
        /// <returns></returns>
        public static bool IsNumber(string quantityStr)
        {
            bool breturn = false;
            int valRet;

            //This should handel all common numbers but not the fractions
            if (Int32.TryParse(quantityStr, out valRet) == true)
                return true;

            switch (quantityStr)
            {
                case "1/2":
                    breturn = true;
                    break;
                case "½":
                    breturn = true;
                    break;
                case "1/4":
                    breturn = true;
                    break;
                case "¼":
                    breturn = true;
                    break;
                case "3/4":
                    breturn = true;
                    break;
                case "¾":
                    breturn = true;
                    break;
                case "1/8":
                    breturn = true;
                    break;
                case "⅛":
                    breturn = true;
                    break;
                case "1/3":
                    breturn = true;
                    break;
                case "⅓":
                    breturn = true;
                    break;
                case "2/3":
                    breturn = true;
                    break;
                case "⅔":
                    breturn = true;
                    break;
            }

            return breturn;
        }

        ///// <summary>
        ///// a convertion from vulgar fractions to a decimal representation in string form
        ///// </summary>
        ///// <param name="fraction"></param>
        ///// <returns></returns>
        //public static string ConvertVulgarFaction(string fraction)
        //{
        //    // we are dealing with an amount so replace the vulgar fractions if they exist
        //    int foundIndex = -1;
        //    string decimalValue;
        //    foundIndex = fraction.IndexOf('½');
        //    if (foundIndex != -1)
        //    {
        //        return ".5";
        //    }

        //    foundIndex = fraction.IndexOf("1/2");
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".5");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf('¼');
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".25");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf("1/4");
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".25");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf('¾');
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".75");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf("3/4");
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".75");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf('⅓');
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".33");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf("1/3");
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".33");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf('⅔');
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".66");
        //        return decimalValue;
        //    }

        //    foundIndex = fraction.IndexOf("2/3");
        //    if (foundIndex != -1)
        //    {
        //        decimalValue = fraction.Remove(foundIndex, 1).Insert(foundIndex, ".66");
        //        return decimalValue;
        //    }

        //    return "-1";
        //}

        /// <summary>
        /// a convertion from vulgar fractions to a decimal representation in string form
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static string ConvertVulgarFaction(string fraction)
        {
            if (fraction.IndexOf('½') != -1 || fraction.IndexOf("1/2") != -1)
            {
                return ".5";
            }

            if (fraction.IndexOf('¼') != -1 || fraction.IndexOf("1/4") != -1)
            {
                return ".25";
            }

            if (fraction.IndexOf('¾') != -1 || fraction.IndexOf("3/4") != -1)
            {
                return ".75";
            }

            if (fraction.IndexOf('⅓') != -1 || fraction.IndexOf("1/3") != -1)
            {
                return ".33";
            }

            if (fraction.IndexOf('⅔') != -1 || fraction.IndexOf("2/3") != -1)
            {
                return ".66";
            }

            return "-1";
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
                case "cup":
                case "cups":
                case "cup(s)":
                    toReturn = "C";
                    break;

                case "teaspoon":
                case "teaspoons":
                case "teaspoon(s)":
                    toReturn = "tsp.";
                    break;

                case "tablespoon":
                case "tablespoons":
                case "tablespoon(s)":
                    toReturn = "Tbsp.";
                    break;
            }

            return toReturn;
        }

        public static string FinalQuantityString(int quantity, string measureType)
        {
            string stringForCompare = measureType.ToLower().Trim();

            if (string.Compare(stringForCompare, "cup") == 0 || (string.Compare(stringForCompare, "cups") == 0) || (string.Compare(stringForCompare, "cup(s)") == 0))
                return quantity.ToString() + " C";

            if (string.Compare(stringForCompare, "teaspoon") == 0 || string.Compare(stringForCompare, "teaspoons") == 0 || string.Compare(stringForCompare, "teaspoon(s)") == 0 ||  string.Compare(stringForCompare, "tsp.") == 0 )
            {
                if (quantity < 3 && quantity > 0)
                {
                    return quantity.ToString() + " tsp.";
                }
                if (quantity >= 3 )
                {
                    int quantityAsInt = quantity / 3;
                    int remainder = quantity % 3;

                    if (quantityAsInt < 4 && remainder > 0)
                        return quantityAsInt.ToString() + " Tbsp. + " + remainder.ToString() + " tsp.";

                    else
                        return quantityAsInt.ToString() + " Tbsp.";
                }
            }

            if (string.Compare(stringForCompare, "tablespoon") == 0 || string.Compare(stringForCompare, "tablespoons") == 0 || string.Compare(stringForCompare, "tablespoon(s)") == 0 || string.Compare(stringForCompare, "Tbsp.") == 0)
            {
                double quantityAsDouble = (double)(quantity);

                if (quantity < 4 && quantity > 0)
                {
                    return quantity.ToString() + " Tbsp.";
                }
                if (quantity >= 4)
                {
                    //the most a given recipe would call for would be 3 tablespoons, 3*5 = 15 tablespoons, so 
                    //we shouldn't ever get more than 15 tablespoons, 16 makes a cup so we should always be under that.
                    int quantityAsInt = quantity / 4;
                    int remainder = quantity % 4;

                    if (quantityAsInt >= 3 && remainder == 0)
                        return "¾ C"; 

                    if (quantityAsInt >= 3 && remainder > 0)
                        return "¾ C + " + remainder.ToString() + " Tbsp.";

                    if (quantityAsInt >= 2 && remainder == 0)
                        return "½ C";

                    if (quantityAsInt >= 2 && remainder > 0)
                        return "½ C + " + remainder.ToString() + " Tbsp.";

                    if (quantityAsInt >= 1 && remainder == 0)
                        return "¼ C";

                    if (quantityAsInt >= 1 && remainder > 0)
                        return "¼ C + " + remainder.ToString() + " Tbsp.";
                } 
            }

            return quantity.ToString();
        }


        /// <summary>
        /// a convertion a decimal representation to a vulgar fraction 
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static string ConvertFloatToVulgarFaction(string FloatToConvert)
        {

            if (FloatToConvert.Contains(".") == false)
            {
                return FloatToConvert;
            }
            
            // we are dealing with a fraction of some kind.
            string[] convertedFloat = FloatToConvert.ToString().Split('.');

            //we don't want a preceeding 0, it looks weird with a vulgar fraction
            if (string.Compare(convertedFloat[0], "0") == 0)
             convertedFloat[0] = ""; 
            
            string fractionToConvert = convertedFloat[1];

            if (string.Compare(fractionToConvert, "25") == 0)
                return convertedFloat[0] + '¼';
           
            if (string.Compare(fractionToConvert, "5") == 0)
                return convertedFloat[0] + '½';
            
            if (string.Compare(fractionToConvert, "75") == 0)
                return convertedFloat[0] + '¾';
            
            //using just the first place of the decimal for the thirds since the second place
            //is less reliable the more they are multipled
            if (string.Compare(fractionToConvert.Substring(0,1) , "3") == 0)
                return convertedFloat[0] + '⅓';
            
            if (string.Compare(fractionToConvert.Substring(0, 1), "6") == 0)
                return convertedFloat[0] + '⅔';

            if (string.Compare(fractionToConvert.Substring(0, 1), "9") == 0)
            {
                int parseInt;
                //parse to an int and then add one and return
                if (Int32.TryParse(convertedFloat[0], out parseInt) == true)
                {
                    return (parseInt + 1).ToString();
                }
                return "-1";
            }
                
            return "-1";
        }

        public static string TryToConvertWritenNumbersToDigits(string amount)
        {
            if (string.Compare(amount.ToLower(), "one") == 0)
                return "1";
            if (string.Compare(amount.ToLower(), "two") == 0)
                return "2";
            if (string.Compare(amount.ToLower(), "three") == 0)
                return "3";
            if (string.Compare(amount.ToLower(), "four") == 0)
                return "4";
            if (string.Compare(amount.ToLower(), "five") == 0)
                return "5";
            if (string.Compare(amount.ToLower(), "six") == 0)
                return "6";
            if (string.Compare(amount.ToLower(), "seven") == 0)
                return "7";
            if (string.Compare(amount.ToLower(), "eight") == 0)
                return "8";
            if (string.Compare(amount.ToLower(), "nine") == 0)
                return "9";
            if (string.Compare(amount.ToLower(), "ten") == 0)
                return "10";
            else
                return "";
        }

        #endregion

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
               .Replace("&egrave;", "è"); 
            returnStr = returnStr.Trim();
            return returnStr;
        }

        #endregion


    }
}
