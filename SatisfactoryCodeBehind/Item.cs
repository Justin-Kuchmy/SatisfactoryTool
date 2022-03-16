using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCodeBehind
{
    public class Item
    {
        private Recipes recipes = new Recipes();
        private List<Item> masterList;
        private static Dictionary<string, double> ResultsList;

        public string name { get; set; }
        public string key_name { get; set; }
        public string category { get; set; }
        public int time { get; set; }
        public List<KeyValuePair<string, int>> ingredients { get; set; }
        public List<KeyValuePair<string, int>> product { get; set; }

        public Dictionary<string, double> RawMaterialsList = new Dictionary<string, double>();


        public Item()
        {
            ResultsList = recipes.GetResults();
        }


        //initializing this items masterList of items.
        public void populateList()
        {
            masterList = recipes.GetList();

        }

        public void CalcRatMats(List<Resource> ResourceList)
        {
            for (int i = 0; i < masterList.Count; i++)
            {
                if (this.name == masterList.ElementAt(i).name)
                {

                    for (int j = 0; j < masterList.ElementAt(i).ingredients.Count; j++)
                    {

                        if (ResourceList.ElementAt(0).resources.Exists((x) => x.key_name == masterList.ElementAt(i).ingredients.ElementAt(j).Key))
                        {
                            var key = masterList.ElementAt(i).ingredients.ElementAt(j).Key;
                            var value = (double)masterList.ElementAt(i).ingredients.ElementAt(j).Value;
                            var returnAmount1 = masterList.ElementAt(i).product.ElementAt(0);
                            if (!this.RawMaterialsList.ContainsKey(key))
                            {
                                this.RawMaterialsList.Add(key, (value / returnAmount1.Value));
                            }
                            else
                            {
                                this.RawMaterialsList[key] += (value / returnAmount1.Value);
                            }
                        }
                        else
                        {
                            var item2 = masterList.ElementAt(i).ingredients.ElementAt(j);
                            if (!ResourceList.ElementAt(0).resources.Exists((x) => x.key_name == item2.Key))
                            {
                                var idx = masterList.FindIndex((x) => x.name == item2.Key);
                                var returnAmount2 = masterList.ElementAt(i).product.ElementAt(0);

                                foreach (var item3 in masterList.ElementAt(idx).RawMaterialsList)
                                {


                                    if (!this.RawMaterialsList.ContainsKey(item3.Key))
                                    {
                                        this.RawMaterialsList.Add(item3.Key, (item2.Value * item3.Value / returnAmount2.Value));
                                    }
                                    else
                                    {
                                        this.RawMaterialsList[item3.Key] += (item2.Value * item3.Value / returnAmount2.Value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
