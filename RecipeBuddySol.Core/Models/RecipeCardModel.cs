using System;
using System.Collections.Generic;


namespace RecipeBuddy.Core.Models
{
    public enum Type_Of_Ingredient { ingredient, subheader, non_consumable }
    public enum Type_Of_Recipe { Appetizer, Beef, Bread, Cake, Candy, Cookie, Custard, Dairy, Eggs, Lamb, Pastery, Pork, Poultry, Salad, Seafood, SideDish, SoupStew, Tofu, Unknown }

    /// <summary>
    /// indicates whether we are adding a recipe to the treeview and DB or removing it
    /// </summary>
    public enum Type_Of_Recipe_Action { Remove, Add, RemoveAll}


    public class RecipeCardModel : ObservableObjBase
    {
        /// <summary>
        /// This constructor is tied to the webscrapping and is called by Scraper.cs to load all the properties
        /// </summary>
        /// <param name="ingredDisplay"></param>
        /// <param name="directDisplay"></param>
        public RecipeCardModel(List<string> ingredDisplay, List<string> directDisplay)
        {
            LoadListSettersWithActionDelegatesForIngredientandDirectionProperties();
            LoadFuncListGettersWithFuncDelegatesForIngredientandDirectionProperties();

            listOfIngredientStringsForDisplay = new List<string>(ingredDisplay);
            listOfDirectionStringsForDisplay = new List<string>(directDisplay);
            SetIngredientAndDirectionProperties();
        }

        /// <summary>
        /// Creating a new RecipeCardModel using the information from another one
        /// </summary>
        /// <param name="recipeCard">The source RecipeCardModel with all the information we need</param>
        public RecipeCardModel(RecipeCardModel reSource)
        {
            LoadListSettersWithActionDelegatesForIngredientandDirectionProperties();
            LoadFuncListGettersWithFuncDelegatesForIngredientandDirectionProperties();
            Title = reSource.Title;
            TotalTime = reSource.TotalTime;
            Author = reSource.Author;
            Website = reSource.Website;
            //Link = reSource.Link;
            //Recipe_Type = reSource.Recipe_Type;
            TypeAsInt = reSource.TypeAsInt;
            ID = reSource.ID;

            //Test for null which will throw and exception before creating the lists!
            if (reSource.listOfIngredientStringsForDisplay != null)
                listOfIngredientStringsForDisplay = new List<string>(reSource.listOfIngredientStringsForDisplay);
            else
            { listOfIngredientStringsForDisplay = new List<string>(); }

            //Test for null which will throw and exception before creating the lists!
            if (reSource.listOfDirectionStringsForDisplay != null)
                listOfDirectionStringsForDisplay = new List<string>(reSource.listOfDirectionStringsForDisplay);
            else
            { listOfDirectionStringsForDisplay = new List<string>(); }

        }

        /// <summary>
        /// the empty RecipeCardModel is used with Dapper when loading information from the DB
        /// and when there is a null entry and a RecipePanel needs to be populated!
        /// </summary>
        public RecipeCardModel()
        {
            LoadListSettersWithActionDelegatesForIngredientandDirectionProperties();
            LoadFuncListGettersWithFuncDelegatesForIngredientandDirectionProperties();
            listOfIngredientStringsForDisplay = new List<string>();
            listOfDirectionStringsForDisplay = new List<string>();

            Title = "Search for your next recipe find!";
            TotalTime = "";
            Author = "";
            Website = "";
            //Link = "";
            TypeAsInt = 0;
            ID = -1;

            listOfIngredientStringsForDisplay = new List<string>();
            listOfDirectionStringsForDisplay = new List<string>();
            SetIngredientAndDirectionProperties();
        }

        /// <summary>
        /// This strings are needed for the Dapper call to the DB, the names are the same as the
        /// names of the columns in the DB recipe table
        /// </summary>
        public string StringOfIngredientForListFromDB;
        public string StringOfDirectionsForListFromDB;

        /// <summary>
        /// Saves the edits to a recipe including a new title
        /// </summary>
        /// <param name="title">The changed title</param>
        public void SaveEditsToARecipe(string title)
        {
            Title = title;
        }
        /// <summary>
        /// Steps requires to update a recipe for saving to the TreeView and DB
        /// </summary>
        public void SaveEditsToARecipe()
        {
            UpdatelistOfIngredientStringsAndDirectionsForDisplayAfterEdits();
            LoadListSettersWithActionDelegatesForIngredientandDirectionProperties();
            SetIngredientAndDirectionProperties();

        }

        /// <summary>
        /// The List of Action delegates that wrap the property-setter for Ingredients
        /// </summary>
        public List<Action<string>> listOfIngredientSetters;

        /// <summary>
        /// The List of Action delegates that wrap the property-setter for Ingredients
        /// </summary>
        public List<Func<string>> listOfIngredientGetters;

        /// <summary>
        /// The List of Action delegates that wrap the property-setter for directions
        /// </summary>
        public List<Action<string>> listOfDirectionSetters;

        /// <summary>
        /// The List of Action delegates that wrap the property-setter for directions
        /// </summary>
        public List<Func<string>> listOfDirectionGetters;

        #region List of 50 Ingredient setters used by the List of Action<string> listOfIngredientSetters

        private void setIngredient1(string value)
        { Ingredient1 = value; }

        private void setIngredient2(string value)
        { Ingredient2 = value; }

        private void setIngredient3(string value)
        { Ingredient3 = value; }

        private void setIngredient4(string value)
        { Ingredient4 = value; }

        private void setIngredient5(string value)
        { Ingredient5 = value; }

        private void setIngredient6(string value)
        { Ingredient6 = value; }

        private void setIngredient7(string value)
        { Ingredient7 = value; }

        private void setIngredient8(string value)
        { Ingredient8 = value; }

        private void setIngredient9(string value)
        { Ingredient9 = value; }

        private void setIngredient10(string value)
        { Ingredient10 = value; }

        private void setIngredient11(string value)
        { Ingredient11 = value; }

        private void setIngredient12(string value)
        { Ingredient12 = value; }

        private void setIngredient13(string value)
        { Ingredient13 = value; }

        private void setIngredient14(string value)
        { Ingredient14 = value; }

        private void setIngredient15(string value)
        { Ingredient15 = value; }

        private void setIngredient16(string value)
        { Ingredient16 = value; }

        private void setIngredient17(string value)
        { Ingredient17 = value; }

        private void setIngredient18(string value)
        { Ingredient18 = value; }

        private void setIngredient19(string value)
        { Ingredient19 = value; }

        private void setIngredient20(string value)
        { Ingredient20 = value; }

        private void setIngredient21(string value)
        { Ingredient21 = value; }

        private void setIngredient22(string value)
        { Ingredient22 = value; }

        private void setIngredient23(string value)
        { Ingredient23 = value; }

        private void setIngredient24(string value)
        { Ingredient24 = value; }

        private void setIngredient25(string value)
        { Ingredient25 = value; }

        private void setIngredient26(string value)
        { Ingredient26 = value; }

        private void setIngredient27(string value)
        { Ingredient27 = value; }

        private void setIngredient28(string value)
        { Ingredient28 = value; }

        private void setIngredient29(string value)
        { Ingredient29 = value; }

        private void setIngredient30(string value)
        { Ingredient30 = value; }

        private void setIngredient31(string value)
        { Ingredient31 = value; }

        private void setIngredient32(string value)
        { Ingredient32 = value; }

        private void setIngredient33(string value)
        { Ingredient33 = value; }

        private void setIngredient34(string value)
        { Ingredient34 = value; }

        private void setIngredient35(string value)
        { Ingredient35 = value; }

        private void setIngredient36(string value)
        { Ingredient36 = value; }

        private void setIngredient37(string value)
        { Ingredient37 = value; }

        private void setIngredient38(string value)
        { Ingredient38 = value; }

        private void setIngredient39(string value)
        { Ingredient39 = value; }

        private void setIngredient40(string value)
        { Ingredient40 = value; }

        private void setIngredient41(string value)
        { Ingredient41 = value; }

        private void setIngredient42(string value)
        { Ingredient42 = value; }

        private void setIngredient43(string value)
        { Ingredient43 = value; }

        private void setIngredient44(string value)
        { Ingredient44 = value; }

        private void setIngredient45(string value)
        { Ingredient45 = value; }

        private void setIngredient46(string value)
        { Ingredient46 = value; }

        private void setIngredient47(string value)
        { Ingredient47 = value; }

        private void setIngredient48(string value)
        { Ingredient48 = value; }

        private void setIngredient49(string value)
        { Ingredient49 = value; }

        private void setIngredient50(string value)
        { Ingredient50 = value; }

        #endregion

        #region List of 50 Ingredient getters used by the List of Func<string> listOfIngredientGetters

        private string getIngredient1()
        { return Ingredient1; }

        private string getIngredient2()
        { return Ingredient2; }

        private string getIngredient3()
        { return Ingredient3; }

        private string getIngredient4()
        { return Ingredient4; }

        private string getIngredient5()
        { return Ingredient5; }

        private string getIngredient6()
        { return Ingredient6; }

        private string getIngredient7()
        { return Ingredient7; }

        private string getIngredient8()
        { return Ingredient8; }

        private string getIngredient9()
        { return Ingredient9; }

        private string getIngredient10()
        { return Ingredient10; }

        private string getIngredient11()
        { return Ingredient11; }

        private string getIngredient12()
        { return Ingredient12; }

        private string getIngredient13()
        { return Ingredient13; }

        private string getIngredient14()
        { return Ingredient14; }

        private string getIngredient15()
        { return Ingredient15; }

        private string getIngredient16()
        { return Ingredient16; }

        private string getIngredient17()
        { return Ingredient17; }

        private string getIngredient18()
        { return Ingredient18; }

        private string getIngredient19()
        { return Ingredient19; }

        private string getIngredient20()
        { return Ingredient20; }

        private string getIngredient21()
        { return Ingredient21; }

        private string getIngredient22()
        { return Ingredient22; }

        private string getIngredient23()
        { return Ingredient23; }

        private string getIngredient24()
        { return Ingredient24; }

        private string getIngredient25()
        { return Ingredient25; }

        private string getIngredient26()
        { return Ingredient26; }

        private string getIngredient27()
        { return Ingredient27; }

        private string getIngredient28()
        { return Ingredient28; }

        private string getIngredient29()
        { return Ingredient29; }

        private string getIngredient30()
        { return Ingredient30; }

        private string getIngredient31()
        { return Ingredient31; }

        private string getIngredient32()
        { return Ingredient32; }

        private string getIngredient33()
        { return Ingredient33; }

        private string getIngredient34()
        { return Ingredient34; }

        private string getIngredient35()
        { return Ingredient35; }

        private string getIngredient36()
        { return Ingredient36; }

        private string getIngredient37()
        { return Ingredient37; }

        private string getIngredient38()
        { return Ingredient38; }

        private string getIngredient39()
        { return Ingredient39; }

        private string getIngredient40()
        { return Ingredient40; }

        private string getIngredient41()
        { return Ingredient41; }

        private string getIngredient42()
        { return Ingredient42; }

        private string getIngredient43()
        { return Ingredient43; }

        private string getIngredient44()
        { return Ingredient44; }

        private string getIngredient45()
        { return Ingredient45; }

        private string getIngredient46()
        { return Ingredient46; }

        private string getIngredient47()
        { return Ingredient47; }

        private string getIngredient48()
        { return Ingredient48; }

        private string getIngredient49()
        { return Ingredient49; }

        private string getIngredient50()
        { return Ingredient50; }

        #endregion

        #region 30 setters used by the List of Action<string> listOfDirectionSetters

        private void setDirection1(string value)
        { Direction1 = value; }

        private void setDirection2(string value)
        { Direction2 = value; }

        private void setDirection3(string value)
        { Direction3 = value; }

        private void setDirection4(string value)
        { Direction4 = value; }

        private void setDirection5(string value)
        { Direction5 = value; }

        private void setDirection6(string value)
        { Direction6 = value; }

        private void setDirection7(string value)
        { Direction7 = value; }

        private void setDirection8(string value)
        { Direction8 = value; }

        private void setDirection9(string value)
        { Direction9 = value; }

        private void setDirection10(string value)
        { Direction10 = value; }

        private void setDirection11(string value)
        { Direction11 = value; }

        private void setDirection12(string value)
        { Direction12 = value; }

        private void setDirection13(string value)
        { Direction13 = value; }

        private void setDirection14(string value)
        { Direction14 = value; }

        private void setDirection15(string value)
        { Direction15 = value; }

        private void setDirection16(string value)
        { Direction16 = value; }

        private void setDirection17(string value)
        { Direction17 = value; }

        private void setDirection18(string value)
        { Direction18 = value; }

        private void setDirection19(string value)
        { Direction19 = value; }

        private void setDirection20(string value)
        { Direction20 = value; }

        private void setDirection21(string value)
        { Direction21 = value; }

        private void setDirection22(string value)
        { Direction22 = value; }

        private void setDirection23(string value)
        { Direction23 = value; }

        private void setDirection24(string value)
        { Direction24 = value; }

        private void setDirection25(string value)
        { Direction25 = value; }

        private void setDirection26(string value)
        { Direction26 = value; }

        private void setDirection27(string value)
        { Direction27 = value; }

        private void setDirection28(string value)
        { Direction28 = value; }

        private void setDirection29(string value)
        { Direction29 = value; }

        private void setDirection30(string value)
        { Direction30 = value; }

        #endregion

        #region 30 getters used by the List of Func<string> listOfDirectionGetters

        private string getDirection1()
        { return Direction1; }

        private string getDirection2()
        { return Direction2; }

        private string getDirection3()
        { return Direction3; }

        private string getDirection4()
        { return Direction4; }

        private string getDirection5()
        { return Direction5; }

        private string getDirection6()
        { return Direction6; }

        private string getDirection7()
        { return Direction7; }

        private string getDirection8()
        { return Direction8; }

        private string getDirection9()
        { return Direction9; }

        private string getDirection10()
        { return Direction10; }

        private string getDirection11()
        { return Direction11; }

        private string getDirection12()
        { return Direction12; }

        private string getDirection13()
        { return Direction13; }

        private string getDirection14()
        { return Direction14; }

        private string getDirection15()
        { return Direction15; }

        private string getDirection16()
        { return Direction16; }

        private string getDirection17()
        { return Direction17; }

        private string getDirection18()
        { return Direction18; }

        private string getDirection19()
        { return Direction19; }

        private string getDirection20()
        { return Direction20; }

        private string getDirection21()
        { return Direction21; }

        private string getDirection22()
        { return Direction22; }

        private string getDirection23()
        { return Direction23; }

        private string getDirection24()
        { return Direction24; }

        private string getDirection25()
        { return Direction25; }

        private string getDirection26()
        { return Direction26; }

        private string getDirection27()
        { return Direction27; }

        private string getDirection28()
        { return Direction28; }

        private string getDirection29()
        { return Direction29; }

        private string getDirection30()
        { return Direction30; }

        #endregion

        /// <summary>
        /// Formatted list of ingredients that includes the section headers and is sent to the property setter for the UI
        /// </summary>
        public List<string> listOfIngredientStringsForDisplay;

        /// <summary>
        /// Formatted list of directions that includes the section headers and is sent to the property setter for the UI
        /// </summary>
        public List<string> listOfDirectionStringsForDisplay;

        #region properties that are linked to the datacontext
        
        public int ID = -1; //the DB number that will be recorded if the recipe is coming from the DB

        //private Type_Of_Recipe type_of_Recipe;
        //public Type_Of_Recipe Recipe_Type
        //{
        //    get { return type_of_Recipe; }
        //    set
        //    {
        //        if (type_of_Recipe == value)
        //            return;

        //        type_of_Recipe = value;
        //        OnPropertyChanged(Recipe_Type.ToString());
        //    }
        //}

        /// <summary>
        /// This is for use by the DB that stores the Type_Of_Recipe as an int but 
        /// can't get it back out and translated to an enum without help
        /// </summary>
        private int typeAsInt;
        public int TypeAsInt
        {
            get { return typeAsInt; }
            set
            {
                if (typeAsInt == value)
                    return;

                typeAsInt = value;
                OnPropertyChanged(TypeAsInt.ToString());
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (string.Compare(title, value) == 0)
                    return;

                title = value;
                OnPropertyChanged(Title);
            }
        }

        //private string link;
        //public string Link
        //{
        //    get { return link; }
        //    set
        //    {
        //        link = value;
        //        OnPropertyChanged(Link);
        //    }
        //}

        //private string description;
        //public string Description
        //{
        //    get { return description; }
        //    set
        //    {
        //        description = value;
        //        OnPropertyChanged(Description);
        //    }
        //}

        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                author = value;
                OnPropertyChanged(Author);
            }
        }

        private string totalTime;
        public string TotalTime
        {
            get { return totalTime; }
            set
            {
                totalTime = value;
                OnPropertyChanged(TotalTime);
            }
        }

        private string website;
        public string Website
        {
            get { return website; }
            set
            {
                website = value;
                OnPropertyChanged(Website);
            }
        }

        #endregion

        #region the private strings for ingredients- 50 - and directions -30- and the List<string> of IngredientValues and DirectionValues

        internal string ingredient1 = ""; private string ingredient2 = ""; private string ingredient3 = ""; private string ingredient4 = ""; private string ingredient5 = ""; private string ingredient6 = ""; private string ingredient7 = ""; private string ingredient8 = ""; private string ingredient9 = ""; private string ingredient10 = "";
        private string ingredient11 = ""; private string ingredient12 = ""; private string ingredient13 = ""; private string ingredient14 = ""; private string ingredient15 = ""; private string ingredient16 = ""; private string ingredient17 = ""; private string ingredient18 = ""; private string ingredient19 = ""; private string ingredient20 = "";
        private string ingredient21 = ""; private string ingredient22 = ""; private string ingredient23 = ""; private string ingredient24 = ""; private string ingredient25 = ""; private string ingredient26 = ""; private string ingredient27 = ""; private string ingredient28 = ""; private string ingredient29 = ""; private string ingredient30 = "";
        private string ingredient31 = ""; private string ingredient32 = ""; private string ingredient33 = ""; private string ingredient34 = ""; private string ingredient35 = ""; private string ingredient36 = ""; private string ingredient37 = ""; private string ingredient38 = ""; private string ingredient39 = ""; private string ingredient40 = "";
        private string ingredient41 = ""; private string ingredient42 = ""; private string ingredient43 = ""; private string ingredient44 = ""; private string ingredient45 = ""; private string ingredient46 = ""; private string ingredient47 = ""; private string ingredient48 = ""; private string ingredient49 = ""; private string ingredient50 = "";

        private string direction1 = ""; private string direction2 = ""; private string direction3 = ""; private string direction4 = ""; private string direction5 = ""; private string direction6 = ""; private string direction7 = ""; private string direction8 = ""; private string direction9 = ""; private string direction10 = "";
        private string direction11 = ""; private string direction12 = ""; private string direction13 = ""; private string direction14 = ""; private string direction15 = ""; private string direction16 = ""; private string direction17 = ""; private string direction18 = ""; private string direction19 = ""; private string direction20 = "";
        private string direction21 = ""; private string direction22 = ""; private string direction23 = ""; private string direction24 = ""; private string direction25 = ""; private string direction26 = ""; private string direction27 = ""; private string direction28 = ""; private string direction29 = ""; private string direction30 = "";
        #endregion

        #region 50 Get/Set Properties for the Ingredients

        public string Ingredient1
        {
            get
            { return ingredient1; }

            set
            {
                ingredient1 = value;
                OnPropertyChanged(Ingredient1);
            }
        }

        public string Ingredient2
        {
            get { return ingredient2; }
            set
            {
                ingredient2 = value;
                OnPropertyChanged(Ingredient2);
            }
        }

        public string Ingredient3
        {
            get { return ingredient3; }

            set
            {
                ingredient3 = value;
                OnPropertyChanged(Ingredient3);
            }
        }

        public string Ingredient4
        {
            get { return ingredient4; }
            set
            {
                ingredient4 = value;
                OnPropertyChanged(Ingredient4);
            }
        }

        public string Ingredient5
        {
            get { return ingredient5; }
            set
            {
                ingredient5 = value;
                OnPropertyChanged(Ingredient5);
            }
        }

        public string Ingredient6
        {
            get { return ingredient6; }
            set
            {
                ingredient6 = value;
                OnPropertyChanged(Ingredient6);
            }
        }

        public string Ingredient7
        {
            get { return ingredient7; }
            set
            {
                ingredient7 = value;
                OnPropertyChanged(Ingredient7);
            }
        }

        public string Ingredient8
        {
            get { return ingredient8; }
            set
            {
                ingredient8 = value;
                OnPropertyChanged(Ingredient8);
            }
        }

        public string Ingredient9
        {
            get { return ingredient9; }
            set
            {
                ingredient9 = value;
                OnPropertyChanged(Ingredient9);
            }
        }

        public string Ingredient10
        {
            get { return ingredient10; }
            set
            {
                ingredient10 = value;
                OnPropertyChanged(Ingredient10);
            }
        }

        public string Ingredient11
        {
            get { return ingredient11; }
            set
            {
                ingredient11 = value;
                OnPropertyChanged(Ingredient11);
            }
        }

        public string Ingredient12
        {
            get { return ingredient12; }
            set
            {
                ingredient12 = value;
                OnPropertyChanged(Ingredient12);
            }
        }

        public string Ingredient13
        {
            get { return ingredient13; }
            set
            {
                ingredient13 = value;
                OnPropertyChanged(Ingredient13);
            }
        }

        public string Ingredient14
        {
            get { return ingredient14; }
            set
            {
                ingredient14 = value;
                OnPropertyChanged(Ingredient14);
            }
        }

        public string Ingredient15
        {
            get { return ingredient15; }
            set
            {
                ingredient15 = value;
                OnPropertyChanged(Ingredient15);
            }
        }

        public string Ingredient16
        {
            get { return ingredient16; }
            set
            {
                ingredient16 = value;
                OnPropertyChanged(Ingredient16);
            }
        }

        public string Ingredient17
        {
            get { return ingredient17; }
            set
            {
                ingredient17 = value;
                OnPropertyChanged(Ingredient17);
            }
        }

        public string Ingredient18
        {
            get { return ingredient18; }
            set
            {
                ingredient18 = value;
                OnPropertyChanged(Ingredient18);
            }
        }

        public string Ingredient19
        {
            get { return ingredient19; }
            set
            {
                ingredient19 = value;
                OnPropertyChanged(Ingredient19);
            }
        }

        public string Ingredient20
        {
            get { return ingredient20; }
            set
            {
                ingredient20 = value;
                OnPropertyChanged(Ingredient20);
            }
        }

        public string Ingredient21
        {
            get { return ingredient21; }
            set
            {
                ingredient21 = value;
                OnPropertyChanged(Ingredient21);
            }
        }

        public string Ingredient22
        {
            get { return ingredient22; }
            set
            {
                ingredient22 = value;
                OnPropertyChanged(Ingredient22);
            }
        }

        public string Ingredient23
        {
            get { return ingredient23; }
            set
            {
                ingredient23 = value;
                OnPropertyChanged(Ingredient23);
            }
        }

        public string Ingredient24
        {
            get { return ingredient24; }
            set
            {
                ingredient24 = value;
                OnPropertyChanged(Ingredient24);
            }
        }

        public string Ingredient25
        {
            get { return ingredient25; }
            set
            {
                ingredient25 = value;
                OnPropertyChanged(Ingredient25);
            }
        }

        public string Ingredient26
        {
            get { return ingredient26; }
            set
            {
                ingredient26 = value;
                OnPropertyChanged(Ingredient26);
            }
        }

        public string Ingredient27
        {
            get { return ingredient27; }
            set
            {
                ingredient27 = value;
                OnPropertyChanged(Ingredient27);
            }
        }

        public string Ingredient28
        {
            get { return ingredient28; }
            set
            {
                ingredient28 = value;
                OnPropertyChanged(Ingredient28);
            }
        }

        public string Ingredient29
        {
            get { return ingredient29; }
            set
            {
                ingredient29 = value;
                OnPropertyChanged(Ingredient29);
            }
        }

        public string Ingredient30
        {
            get { return ingredient30; }
            set
            {
                ingredient30 = value;
                OnPropertyChanged(Ingredient30);
            }
        }

        public string Ingredient31
        {
            get { return ingredient31; }
            set
            {
                ingredient31 = value;
                OnPropertyChanged(Ingredient31);
            }
        }

        public string Ingredient32
        {
            get { return ingredient32; }
            set
            {
                ingredient32 = value;
                OnPropertyChanged(Ingredient32);
            }
        }

        public string Ingredient33
        {
            get { return ingredient33; }
            set
            {
                ingredient33 = value;
                OnPropertyChanged(Ingredient33);
            }
        }

        public string Ingredient34
        {
            get { return ingredient34; }
            set
            {
                ingredient34 = value;
                OnPropertyChanged(Ingredient34);
            }
        }

        public string Ingredient35
        {
            get { return ingredient35; }
            set
            {
                ingredient35 = value;
                OnPropertyChanged(Ingredient35);
            }
        }

        public string Ingredient36
        {
            get { return ingredient36; }
            set
            {
                ingredient36 = value;
                OnPropertyChanged(Ingredient36);
            }
        }

        public string Ingredient37
        {
            get { return ingredient37; }
            set
            {
                ingredient37 = value;
                OnPropertyChanged(Ingredient37);
            }
        }

        public string Ingredient38
        {
            get { return ingredient38; }
            set
            {
                ingredient38 = value;
                OnPropertyChanged(Ingredient38);
            }
        }

        public string Ingredient39
        {
            get { return ingredient39; }
            set
            {
                ingredient39 = value;
                OnPropertyChanged(Ingredient39);
            }
        }

        public string Ingredient40
        {
            get { return ingredient40; }
            set
            {
                ingredient40 = value;
                OnPropertyChanged(Ingredient40);
            }
        }

        public string Ingredient41
        {
            get { return ingredient41; }
            set
            {
                ingredient41 = value;
                OnPropertyChanged(Ingredient41);
            }
        }

        public string Ingredient42
        {
            get { return ingredient42; }
            set
            {
                ingredient42 = value;
                OnPropertyChanged(Ingredient42);
            }
        }

        public string Ingredient43
        {
            get { return ingredient43; }
            set
            {
                ingredient43 = value;
                OnPropertyChanged(Ingredient43);
            }
        }

        public string Ingredient44
        {
            get { return ingredient44; }
            set
            {
                ingredient44 = value;
                OnPropertyChanged(Ingredient44);
            }
        }

        public string Ingredient45
        {
            get { return ingredient45; }
            set
            {
                ingredient45 = value;
                OnPropertyChanged(Ingredient45);
            }
        }

        public string Ingredient46
        {
            get { return ingredient46; }
            set
            {
                ingredient46 = value;
                OnPropertyChanged(Ingredient46);
            }
        }

        public string Ingredient47
        {
            get { return ingredient47; }
            set
            {
                ingredient47 = value;
                OnPropertyChanged(Ingredient47);
            }
        }

        public string Ingredient48
        {
            get { return ingredient48; }
            set
            {
                ingredient48 = value;
                OnPropertyChanged(Ingredient48);
            }
        }

        public string Ingredient49
        {
            get { return ingredient49; }
            set
            {
                ingredient49 = value;
                OnPropertyChanged(Ingredient49);
            }
        }

        public string Ingredient50
        {
            get { return ingredient50; }
            set
            {
                ingredient50 = value;
                OnPropertyChanged(Ingredient50);
            }
        }
        #endregion

        #region 30 Get/Set Properties for the Directions

        //Directions - 30 Get/Set and accessor set for the Action delegates
        public string Direction1
        {
            get { return direction1; }
            set { direction1 = value; OnPropertyChanged(Direction1); }
        }

        public string Direction2
        {
            get { return direction2; }
            set { direction2 = value; OnPropertyChanged(Direction2); }
        }

        public string Direction3
        {
            get { return direction3; }
            set { direction3 = value; OnPropertyChanged(Direction3); }
        }

        public string Direction4
        {
            get { return direction4; }
            set { direction4 = value; OnPropertyChanged(Direction4); }
        }

        public string Direction5
        {
            get { return direction5; }
            set { direction5 = value; OnPropertyChanged(Direction5); }
        }

        public string Direction6
        {
            get { return direction6; }
            set { direction6 = value; OnPropertyChanged(Direction6); }
        }

        public string Direction7
        {
            get { return direction7; }
            set { direction7 = value; OnPropertyChanged(Direction7); }
        }

        public string Direction8
        {
            get { return direction8; }
            set { direction8 = value; OnPropertyChanged(Direction8); }
        }

        public string Direction9
        {
            get { return direction9; }
            set { direction9 = value; OnPropertyChanged(Direction9); }
        }

        public string Direction10
        {
            get { return direction10; }
            set { direction10 = value; OnPropertyChanged(Direction10); }
        }

        public string Direction11
        {
            get { return direction11; }
            set { direction11 = value; OnPropertyChanged(Direction11); }
        }

        public string Direction12
        {
            get { return direction12; }
            set { direction12 = value; OnPropertyChanged(Direction12); }
        }

        public string Direction13
        {
            get { return direction13; }
            set { direction13 = value; OnPropertyChanged(Direction13); }
        }

        public string Direction14
        {
            get { return direction14; }
            set { direction14 = value; OnPropertyChanged(Direction14); }
        }

        public string Direction15
        {
            get { return direction15; }
            set { direction15 = value; OnPropertyChanged(Direction15); }
        }

        public string Direction16
        {
            get { return direction16; }
            set { direction16 = value; OnPropertyChanged(Direction16); }
        }

        public string Direction17
        {
            get { return direction17; }
            set { direction17 = value; OnPropertyChanged(Direction17); }
        }

        public string Direction18
        {
            get { return direction18; }
            set { direction18 = value; OnPropertyChanged(Direction18); }
        }

        public string Direction19
        {
            get { return direction19; }
            set { direction19 = value; OnPropertyChanged(Direction19); }
        }

        public string Direction20
        {
            get { return direction20; }
            set { direction20 = value; OnPropertyChanged(Direction20); }
        }

        public string Direction21
        {
            get { return direction21; }
            set { direction21 = value; OnPropertyChanged(Direction21); }
        }

        public string Direction22
        {
            get { return direction22; }
            set { direction22 = value; OnPropertyChanged(Direction22); }
        }

        public string Direction23
        {
            get { return direction23; }
            set { direction23 = value; OnPropertyChanged(Direction23); }
        }

        public string Direction24
        {
            get { return direction24; }
            set { direction24 = value; OnPropertyChanged(Direction24); }
        }

        public string Direction25
        {
            get { return direction25; }
            set { direction25 = value; OnPropertyChanged(Direction25); }
        }

        public string Direction26
        {
            get { return direction26; }
            set { direction26 = value; OnPropertyChanged(Direction26); }
        }

        public string Direction27
        {
            get { return direction27; }
            set { direction27 = value; OnPropertyChanged(Direction27); }
        }

        public string Direction28
        {
            get { return direction28; }
            set { direction8 = value; OnPropertyChanged(Direction28); }
        }

        public string Direction29
        {
            get { return direction29; }
            set { direction29 = value; OnPropertyChanged(Direction29); }
        }

        public string Direction30
        {
            get { return direction30; }
            set { direction30 = value; OnPropertyChanged(Direction30); }
        }

        #endregion

        /// <summary>
        /// Copying over the data from another RecipeCardModel to this one
        /// </summary>
        /// <param name="reSource"></param>
        public void CopyRecipeCardModel(RecipeCardModel reSource)
        {
            if (reSource == null)
            { return; }
            Title = reSource.Title;
            TotalTime = reSource.TotalTime;
            Author = reSource.Author;
            Website = reSource.Website;
            //Link = reSource.Link;
            TypeAsInt = reSource.TypeAsInt;
            ID = reSource.ID;

            if (reSource.listOfIngredientStringsForDisplay != null)
                listOfIngredientStringsForDisplay = new List<string>(reSource.listOfIngredientStringsForDisplay);
            else
            { listOfIngredientStringsForDisplay = new List<string>(); }

            if (reSource.listOfDirectionStringsForDisplay != null)
                listOfDirectionStringsForDisplay = new List<string>(reSource.listOfDirectionStringsForDisplay);
            else
            { listOfDirectionStringsForDisplay = new List<string>(); }

            SetIngredientAndDirectionProperties();
        }

        /// Updates the various elements of the RecipeEntry from the next entry in the RecipeEntriesList, triggering the OnPropertyChanged event that is "contextbound" to the UI
        /// Adds the ingredents and Directions dynamically and then deletes them before the new recipe is replacing the old one.
        /// </summary>
        /// <param name="reSource">The new RecipeCard which we will use to overwrite the old values</param>
        public void UpdateRecipeEntry(RecipeCardModel reSource)
        {
            CopyRecipeCardModel(reSource);
        }

        /// <summary>
        /// Users list.count to return the next index to the caller
        /// </summary>
        /// <returns>returns the lowest empty index entry to the caller - for use with the "add ingredients" editing</returns>
        public int GetNextEmptyIngredientIndex()
        {
            if (listOfIngredientStringsForDisplay.Count < 50)
                return listOfIngredientStringsForDisplay.Count;
            else
                return -1;
        }

        /// <summary>
        /// Users list.count to return the next index to the caller
        /// </summary>
        /// <returns>returns the lowest empty index entry to the caller - for use with the "add directions" editing</returns>
        public int GetNextEmptyDirectionIndex()
        {
            if (listOfDirectionStringsForDisplay.Count < 30)
                return listOfDirectionStringsForDisplay.Count;
            else
                return -1;
        }

        /// <summary>
        /// Updates the raw ingredients and listOfIngredientStringForDisplay with the new ingredient just added
        /// </summary>
        /// <param name="newIngredients"></param>
        private void AddNewIgredientTextBox(string newIngredients)
        {
            listOfIngredientStringsForDisplay.Add(newIngredients);
            int count = listOfIngredientStringsForDisplay.Count;
            listOfIngredientSetters[count - 1].Invoke(newIngredients);
        }

        /// <summary>
        /// Updates the raw directions and listOfDirectionStringsForDisplay with the new entry
        /// </summary>
        /// <param name="newIngredients"></param>
        private void AddNewDirectionTextBox(string newDirections)
        {
            listOfDirectionStringsForDisplay.Add(newDirections);
            int count = listOfIngredientStringsForDisplay.Count;
            listOfDirectionSetters[count - 1].Invoke(newDirections);
        }

        /// <summary>
        /// Removes the last ingredient added to both the raw ingredients and listOfIngredientStringForDisplay 
        /// </summary>
        private void RemoveLastIngredientInList()
        {
            if (listOfIngredientStringsForDisplay.Count > 1)
                listOfIngredientStringsForDisplay.RemoveAt(listOfIngredientStringsForDisplay.Count - 1);
        }

        /// <summary>
        /// Removes the last direction added to both the raw directions and listOfDirectionStringsforDisplay
        /// </summary>
        private void RemoveLastDirectionInList()
        {
            if (listOfDirectionStringsForDisplay.Count > 1)
                listOfDirectionStringsForDisplay.RemoveAt(listOfDirectionStringsForDisplay.Count - 1);
        }

        /// <summary>
        /// Loading the 50 ingredient and 30 direction properties with a list of action-setters.
        /// </summary>
        private void LoadListSettersWithActionDelegatesForIngredientandDirectionProperties()
        {
            listOfIngredientSetters = new List<Action<string>>();
            #region loading the listOfIngredientSetters with Action delegates
            listOfIngredientSetters.Add(setIngredient1); listOfIngredientSetters.Add(setIngredient2); listOfIngredientSetters.Add(setIngredient3); listOfIngredientSetters.Add(setIngredient4); listOfIngredientSetters.Add(setIngredient5);
            listOfIngredientSetters.Add(setIngredient6); listOfIngredientSetters.Add(setIngredient7); listOfIngredientSetters.Add(setIngredient8); listOfIngredientSetters.Add(setIngredient9); listOfIngredientSetters.Add(setIngredient10);
            listOfIngredientSetters.Add(setIngredient11); listOfIngredientSetters.Add(setIngredient12); listOfIngredientSetters.Add(setIngredient13); listOfIngredientSetters.Add(setIngredient14); listOfIngredientSetters.Add(setIngredient15);
            listOfIngredientSetters.Add(setIngredient16); listOfIngredientSetters.Add(setIngredient17); listOfIngredientSetters.Add(setIngredient18); listOfIngredientSetters.Add(setIngredient19); listOfIngredientSetters.Add(setIngredient20);
            listOfIngredientSetters.Add(setIngredient21); listOfIngredientSetters.Add(setIngredient22); listOfIngredientSetters.Add(setIngredient23); listOfIngredientSetters.Add(setIngredient24); listOfIngredientSetters.Add(setIngredient25);
            listOfIngredientSetters.Add(setIngredient26); listOfIngredientSetters.Add(setIngredient27); listOfIngredientSetters.Add(setIngredient28); listOfIngredientSetters.Add(setIngredient29); listOfIngredientSetters.Add(setIngredient30);
            listOfIngredientSetters.Add(setIngredient31); listOfIngredientSetters.Add(setIngredient32); listOfIngredientSetters.Add(setIngredient33); listOfIngredientSetters.Add(setIngredient34); listOfIngredientSetters.Add(setIngredient35);
            listOfIngredientSetters.Add(setIngredient36); listOfIngredientSetters.Add(setIngredient37); listOfIngredientSetters.Add(setIngredient38); listOfIngredientSetters.Add(setIngredient39); listOfIngredientSetters.Add(setIngredient40);
            listOfIngredientSetters.Add(setIngredient41); listOfIngredientSetters.Add(setIngredient42); listOfIngredientSetters.Add(setIngredient43); listOfIngredientSetters.Add(setIngredient44); listOfIngredientSetters.Add(setIngredient45);
            listOfIngredientSetters.Add(setIngredient46); listOfIngredientSetters.Add(setIngredient47); listOfIngredientSetters.Add(setIngredient48); listOfIngredientSetters.Add(setIngredient49); listOfIngredientSetters.Add(setIngredient50);
            #endregion

            listOfDirectionSetters = new List<Action<string>>();
            #region listOfDirectionSetters with Action delegates
            listOfDirectionSetters.Add(setDirection1); listOfDirectionSetters.Add(setDirection2); listOfDirectionSetters.Add(setDirection3); listOfDirectionSetters.Add(setDirection4); listOfDirectionSetters.Add(setDirection5);
            listOfDirectionSetters.Add(setDirection6); listOfDirectionSetters.Add(setDirection7); listOfDirectionSetters.Add(setDirection8); listOfDirectionSetters.Add(setDirection9); listOfDirectionSetters.Add(setDirection10);
            listOfDirectionSetters.Add(setDirection11); listOfDirectionSetters.Add(setDirection12); listOfDirectionSetters.Add(setDirection13); listOfDirectionSetters.Add(setDirection14); listOfDirectionSetters.Add(setDirection15);
            listOfDirectionSetters.Add(setDirection16); listOfDirectionSetters.Add(setDirection17); listOfDirectionSetters.Add(setDirection18); listOfDirectionSetters.Add(setDirection19); listOfDirectionSetters.Add(setDirection20);
            listOfDirectionSetters.Add(setDirection21); listOfDirectionSetters.Add(setDirection22); listOfDirectionSetters.Add(setDirection23); listOfDirectionSetters.Add(setDirection24); listOfDirectionSetters.Add(setDirection25);
            listOfDirectionSetters.Add(setDirection26); listOfDirectionSetters.Add(setDirection27); listOfDirectionSetters.Add(setDirection28); listOfDirectionSetters.Add(setDirection29); listOfDirectionSetters.Add(setDirection30);
            #endregion}
        }


        /// <summary>
        /// Loading the 50 ingredient and 30 direction properties with a list of Func-Getters.
        /// </summary>
        public void LoadFuncListGettersWithFuncDelegatesForIngredientandDirectionProperties()
        {
            listOfIngredientGetters = new List<Func<string>>();
            #region loading the listOfIngredientSetters with Func delegates
            listOfIngredientGetters.Add(getIngredient1); listOfIngredientGetters.Add(getIngredient2); listOfIngredientGetters.Add(getIngredient3); listOfIngredientGetters.Add(getIngredient4); listOfIngredientGetters.Add(getIngredient5);
            listOfIngredientGetters.Add(getIngredient6); listOfIngredientGetters.Add(getIngredient7); listOfIngredientGetters.Add(getIngredient8); listOfIngredientGetters.Add(getIngredient9); listOfIngredientGetters.Add(getIngredient10);
            listOfIngredientGetters.Add(getIngredient11); listOfIngredientGetters.Add(getIngredient12); listOfIngredientGetters.Add(getIngredient13); listOfIngredientGetters.Add(getIngredient14); listOfIngredientGetters.Add(getIngredient15);
            listOfIngredientGetters.Add(getIngredient16); listOfIngredientGetters.Add(getIngredient17); listOfIngredientGetters.Add(getIngredient18); listOfIngredientGetters.Add(getIngredient19); listOfIngredientGetters.Add(getIngredient20);
            listOfIngredientGetters.Add(getIngredient21); listOfIngredientGetters.Add(getIngredient22); listOfIngredientGetters.Add(getIngredient23); listOfIngredientGetters.Add(getIngredient24); listOfIngredientGetters.Add(getIngredient25);
            listOfIngredientGetters.Add(getIngredient26); listOfIngredientGetters.Add(getIngredient27); listOfIngredientGetters.Add(getIngredient28); listOfIngredientGetters.Add(getIngredient29); listOfIngredientGetters.Add(getIngredient30);
            listOfIngredientGetters.Add(getIngredient31); listOfIngredientGetters.Add(getIngredient32); listOfIngredientGetters.Add(getIngredient33); listOfIngredientGetters.Add(getIngredient34); listOfIngredientGetters.Add(getIngredient35);
            listOfIngredientGetters.Add(getIngredient36); listOfIngredientGetters.Add(getIngredient37); listOfIngredientGetters.Add(getIngredient38); listOfIngredientGetters.Add(getIngredient39); listOfIngredientGetters.Add(getIngredient40);
            listOfIngredientGetters.Add(getIngredient41); listOfIngredientGetters.Add(getIngredient42); listOfIngredientGetters.Add(getIngredient43); listOfIngredientGetters.Add(getIngredient44); listOfIngredientGetters.Add(getIngredient45);
            listOfIngredientGetters.Add(getIngredient46); listOfIngredientGetters.Add(getIngredient47); listOfIngredientGetters.Add(getIngredient48); listOfIngredientGetters.Add(getIngredient49); listOfIngredientGetters.Add(getIngredient50);
            #endregion

            listOfDirectionGetters = new List<Func<string>>();
            #region listOfDirectionGetters with Func delegates
            listOfDirectionGetters.Add(getDirection1); listOfDirectionGetters.Add(getDirection2); listOfDirectionGetters.Add(getDirection3); listOfDirectionGetters.Add(getDirection4); listOfDirectionGetters.Add(getDirection5);
            listOfDirectionGetters.Add(getDirection6); listOfDirectionGetters.Add(getDirection7); listOfDirectionGetters.Add(getDirection8); listOfDirectionGetters.Add(getDirection9); listOfDirectionGetters.Add(getDirection10);
            listOfDirectionGetters.Add(getDirection11); listOfDirectionGetters.Add(getDirection12); listOfDirectionGetters.Add(getDirection13); listOfDirectionGetters.Add(getDirection14); listOfDirectionGetters.Add(getDirection15);
            listOfDirectionGetters.Add(getDirection16); listOfDirectionGetters.Add(getDirection17); listOfDirectionGetters.Add(getDirection18); listOfDirectionGetters.Add(getDirection19); listOfDirectionGetters.Add(getDirection20);
            listOfDirectionGetters.Add(getDirection21); listOfDirectionGetters.Add(getDirection22); listOfDirectionGetters.Add(getDirection23); listOfDirectionGetters.Add(getDirection24); listOfDirectionGetters.Add(getDirection25);
            listOfDirectionGetters.Add(getDirection26); listOfDirectionGetters.Add(getDirection27); listOfDirectionGetters.Add(getDirection28); listOfDirectionGetters.Add(getDirection29); listOfDirectionGetters.Add(getDirection30);
            #endregion}
        }

        /// <summary>
        /// invokes the preloaded list of Action delegates for both the ingredients and directions lists
        /// </summary>
        public void SetIngredientAndDirectionProperties()
        {
            //clean out whatever might be leftover
            for (int count = 0; count < 50; count++)
            {
                listOfIngredientSetters[count].Invoke("");
            }

            for (int count = 0; count < 30; count++)
            {
                listOfDirectionSetters[count].Invoke("");
            }

            //The first part of this function just set all of the ingredients and directions to empty strings
            //so all the loops have to expect that all 50 strings exist.  Now we need to test to see if they are blank or not?
            for (int IngredientCount = 0; listOfIngredientStringsForDisplay.Count > IngredientCount; IngredientCount++)
            {
                if (listOfIngredientStringsForDisplay[IngredientCount].Length > 0 && IngredientCount < 50)
                {
                        listOfIngredientSetters[IngredientCount].Invoke(listOfIngredientStringsForDisplay[IngredientCount]);
                }
            }

            for (int directionCount = 0; listOfDirectionStringsForDisplay.Count > directionCount; directionCount++)
            {
                if (listOfDirectionStringsForDisplay[directionCount].Length > 0 && directionCount < 30)
                {
                    listOfDirectionSetters[directionCount].Invoke(listOfDirectionStringsForDisplay[directionCount]);
                }
            }
        }

        /// <summary>
        /// Pulls the list of ingredients from the active textboxes on the Edits page and turns 
        /// into a List<string> which is saved as the new listOfIngredientStringsForDisplay
        /// and listOfDirectionStringsForDisplay of the RecipeCardModel
        /// </summary>
        /// <returns>The string to be added to the DB</returns>
        private void UpdatelistOfIngredientStringsAndDirectionsForDisplayAfterEdits()
        { 
            List<string> newListIngredients = new List<string>();

            for (int count = 0; count < 50; count++)
            {
                newListIngredients.Add(listOfIngredientGetters[count].Invoke());
            }

            listOfIngredientStringsForDisplay = new List<string>(newListIngredients);


            List<string> newListDirections = new List<string>();

            for (int count = 0; count < 30; count++)
            {
                newListDirections.Add(listOfDirectionGetters[count].Invoke());
            }

            listOfDirectionStringsForDisplay = new List<string>(newListDirections);
        }
    }
}
