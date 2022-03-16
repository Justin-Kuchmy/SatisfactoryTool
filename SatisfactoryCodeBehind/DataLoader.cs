using System;
using System.IO;                                        //File Stream Class
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;


namespace SatisfactoryCodeBehind
{
    public class DataLoader
    {
        private const string RECIPES_FILE = "../../../data/Recipes.json";
        private const string MATERIALS_FILE = "../../../data/Resources.json";

        public List<Resource> ResourceList { get; }
        public List<Recipes> RecipesList { get; }
        public List<Item> itemList { get; }
        public List<KeyValuePair<string, int>> ingredientList { get; set; }
        public List<KeyValuePair<string, int>> ReturnedList { get; set; }
        public IEnumerable<JToken> TheRecipe { get; set; }
        public JObject RecipeData { get; set; }

        public DataLoader()
        {
            ResourceList = new List<Resource>();
            RecipesList = new List<Recipes>();
            itemList = new List<Item>();
            ResourceList = ReadJsonFileToLib_Resource(MATERIALS_FILE);
            RecipeData = loadJsonData_Recipes();
            TheRecipe = RecipeData.SelectTokens($"$..recipes[*]");
            this.populateList();

        }

        //generates the master list of items from the Json file
        public void populateList()
        {
            if (TheRecipe.GetEnumerator().MoveNext()) // true if not an empty collection
            {
                Item theitem = null;
                int i = 0;
                foreach (JToken Recipe in TheRecipe)
                {
                    theitem = new Item();

                    var itemName = Recipe.Value<string>("name");
                    theitem.name = itemName;
                    var itemkey_name = Recipe.Value<string>("key_name");
                    theitem.key_name = itemkey_name;
                    var itemcategory = Recipe.Value<string>("category");
                    theitem.category = itemcategory;
                    var itemtime = Recipe.Value<string>("time");
                    theitem.time = Int32.Parse(itemtime);
                    IEnumerable<JToken> ingredients = RecipeData.SelectTokens($"$..recipes[{i}].ingredients[*]");
                    IEnumerable<JToken> Returned = RecipeData.SelectTokens($"$..recipes[{i}].product[*]");

                    ingredientList = new List<KeyValuePair<string, int>>();
                    KeyValuePair<string, int> ing;
                    foreach (JToken ingredient in ingredients)
                    {
                        var item = ingredient.Value<Object>(0).ToString();
                        var itemCount = Int32.Parse(ingredient.Value<Object>(1).ToString());
                        ing = new KeyValuePair<string, int>(item, itemCount);
                        ingredientList.Add(ing);
                    }

                    theitem.ingredients = ingredientList;


                    ReturnedList = new List<KeyValuePair<string, int>>();
                    KeyValuePair<string, int> Rtn;
                    foreach (JToken returned in Returned)
                    {
                        var item = returned.Value<Object>(0).ToString();
                        var itemCount = Int32.Parse(returned.Value<Object>(1).ToString());
                        Rtn = new KeyValuePair<string, int>(item, itemCount);
                        ReturnedList.Add(Rtn);
                    }

                    theitem.product = ReturnedList;
                    itemList.Add(theitem);
                    i++;

                }

                Recipes newRecipes = new Recipes(itemList);
                RecipesList.Add(newRecipes);

                //each item has a reference to the entire list of 
                foreach (var item in Recipes.recipes)
                {
                    item.populateList();
                }


            }
            //populating each items raw material list 
            foreach (var item in itemList)
            {
                item.CalcRatMats(ResourceList);
            }
        }

        //calculates the items per x minutes based on input then grabs the raw materials based on 
        //that number of items and returns the data. 
        public Dictionary<string, double> getItem(string input, double itemPerMinute)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();

            var itemToFind = itemList.FindIndex((x) => x.name == input);
            double multiplier = 1.0;
            var itemsMade = (60.0 / itemList.ElementAt(itemToFind).time);
            var timeChange = itemList.ElementAt(itemToFind).time / 60.0;

            var number = 0.0;
            if (itemPerMinute != 0)
            {
                number = itemPerMinute;
            }
            else
            {
                number = itemsMade;
            }
            if (number > itemsMade)
            {
                multiplier = (number / itemsMade);

            }


            foreach (var item2 in itemList.ElementAt(itemToFind).RawMaterialsList)
            {
                var result = (Math.Round((item2.Value / timeChange) * 100) / 100) * multiplier;
                if (dict.ContainsKey(item2.Key))
                {
                    dict[item2.Key] += result;
                }
                else
                {
                    dict[item2.Key] = result;
                }

            }

            return dict;
        }

        private static JObject loadJsonData_Recipes()
        {
            string json = File.ReadAllText(RECIPES_FILE);
            return JObject.Parse(json);
        } // end loadJsonData()
        private static List<Resource> ReadJsonFileToLib_Resource(string FILE)
        {
            //returns a string to read all the data from the json file
            string json = File.ReadAllText(FILE);
            var result = JsonConvert.DeserializeObject<List<Resource>>(json);
            return result;
        }

    }
}
