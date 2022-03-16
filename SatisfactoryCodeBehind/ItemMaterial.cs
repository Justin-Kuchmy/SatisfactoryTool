using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCodeBehind
{
    public class ItemMaterial
    {
        public string key_name { get; set; }
        public string category { get; set; }

        public ItemMaterial(string key_name, string category)
        {

            this.key_name = key_name;
            this.category = category;

        }

    }
}
