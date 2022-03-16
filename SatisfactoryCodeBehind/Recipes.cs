using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCodeBehind
{
    public class Recipes
    {
        public static List<Item> recipes { set; get; }
        public static Dictionary<string, double> ResultsList { set; get; }

        public Recipes()
        {
            recipes = new List<Item>();
            ResultsList = new Dictionary<string, double>();
        }

        public Recipes(List<Item> recipess)
        {
            recipes = recipess;

        }

        public List<Item> GetList()
        {
            return recipes;
        }
        public Dictionary<string, double> GetResults()
        {
            return ResultsList;
        }

        public int Count()
        {
            return recipes.Count();
        }


    }
}
