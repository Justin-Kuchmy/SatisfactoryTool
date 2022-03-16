using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SatisfactoryCodeBehind;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SatisfactoryItemCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Resource resource;
        public Recipes recipes;
        public Item item;
        public DataLoader data;
        public string itemString = "";
        public double itemsPerMin = 0.0;
        public Dictionary<string, double> currDict = new Dictionary<string, double>(0);
        public Dictionary<string, double> ListOfAdded = new Dictionary<string, double>(0);

        public MainWindow()
        {
            InitializeComponent();
            resource = new Resource();
            item = new Item();
            recipes = new Recipes();
            data = new DataLoader();
            LoadItemNames();
        }

        private void LoadItemNames()
        {
            List<string> Source = new List<string>();
            for (int i = 0; i < recipes.Count(); i++)
            {
                //ItemSelected.ItemsSource = new List<string> { "Alice", "Bob", "Charlie" };
                Source.Add(recipes.GetList().ElementAt(i).name);
               
            }

            ItemSelected.ItemsSource = Source;


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Dictionary to hold the current item to add   
            Dictionary<string, double> dict = new Dictionary<string, double>(0);
            dict = data.getItem(itemString, itemsPerMin);
            double tempItemsPerMin = itemsPerMin;
            if (itemsPerMin == 0)
            {
                int itemIdx = recipes.GetList().FindIndex((x) => x.name == itemString);
                var itemObject = recipes.GetList().ElementAt(itemIdx);
                tempItemsPerMin = 60.0 / itemObject.time;
            }

            if (ListOfAdded.ContainsKey(itemString))
            {
                ListOfAdded[itemString] += tempItemsPerMin;
            }
            else
            {
                ListOfAdded[itemString] = tempItemsPerMin;
            }

            string inputlist = "";
            var count = ListOfAdded.Count;
            int index = 0;
            //formatting the first string in the list 
            foreach (var item in ListOfAdded)
            {
                if (index == 0)
                {
                    inputlist += $"{item.Value} {item.Key}'s";
                }
                else if (index == count - 1)
                {
                    inputlist += $" and {item.Value} {item.Key}'s";
                }
                else 
                    inputlist += $", {item.Value} {item.Key}'s";
                index++;
            }
            inputlist += $" Per Minute";



            //Combining the dict with the newest item with the 'currDict' which has the previouly
            //added items 
            foreach (var item in dict)
            {
                if (currDict.ContainsKey(item.Key))
                {
                    currDict[item.Key] += Math.Round(item.Value);
                }
                else
                {
                    currDict[item.Key] = Math.Round(item.Value);
                }
            }
            //clear the list 
            ItemList.Items.Clear();

            ItemList.Items.Add(inputlist);
            //add list to the itemlist
            foreach (var item in currDict)
            {
                ItemList.Items.Add(item);
            }

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Clear();
            currDict = new Dictionary<string, double>();
            ItemsPerMin.Text = "0";
        }


        //TextBox
        private void ItemsPerMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ItemsPerMin.Text == "") ItemsPerMin.Text = "0";
            itemsPerMin = double.Parse(ItemsPerMin.Text);
        }

        //textBox validation, ensures only numbers can be entered. 
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //ComboBox
        private void ItemSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemString = e.AddedItems[0].ToString();
        }

        //ListBox
        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
