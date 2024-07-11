using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zaklad.Models;

namespace Zaklad
{
    internal class MainPageViewModel
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public string Proteins { get; private set; }
        public string Fat { get; private set; }
        public string Carbohydrates { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        Dictionary<string, int> ButtonDays = new Dictionary<string, int>()
        {
            { "Nd", 0},
            { "Pon", 1},
            { "Wt", 2},
            { "Śr", 3},
            { "Czw", 4},
            { "Pt", 5},
            { "Sob", 6},
        };
        public ICommand ChangeDateOfWeekCommand => new Command(ChangeDayOfWeek);
        public MainPageViewModel()
        {
            this.DayOfWeek = DateTime.Now.DayOfWeek;
            GetProductsCollection();
        }
        private void ChangeDayOfWeek(object obj)
        { 
             this.DayOfWeek = (DayOfWeek)ButtonDays[obj as string];
        }
        private void GetProductsCollection()
        {
            List<Product> products = UserProductsData.GetProducts("All.txt");
            Products.CollectionChanged += new NotifyCollectionChangedEventHandler(CalculateMakro);
            if (products != null)
            {
                foreach (Product product in products)
                {
                    Products.Add(product);
                }
            }
        }
        private void GetProductCollectionByDate()
        { 
            
        }
        private void CalculateMakro(object sender, NotifyCollectionChangedEventArgs e)
        {
            decimal proteins = 0;
            decimal fat = 0;
            decimal carbohydrates = 0;
            foreach (var i in Products)
            {
                proteins += i.Proteins;
                fat += i.Fat;
                carbohydrates += i.Carbohydrates;
            }
            Proteins = proteins.ToString() + "g";
            Fat = fat.ToString() + "g";
            Carbohydrates = carbohydrates.ToString() + "g";
        }
    }
}
