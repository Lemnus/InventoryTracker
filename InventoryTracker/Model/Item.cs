using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Model
{
    public class Item : ObservableObject
    {
        protected string _name;
        protected int _cost;
        protected int _stockCount;

        public string Name 
        { 
            get { return _name; }
            set
            {
                Set<string>(() => this.Name, ref _name, value);
            }
        }
        public int Cost
        {
            get { return _cost; }
            set
            {
                Set<int>(() => this.Cost, ref _cost, value);
            }
        }
        public int StockCount
        {
            get { return _stockCount; }
            set
            {
                Set<int>(() => this.StockCount, ref _stockCount, value);
            }
        }
        public Item(string name="Test", int cost=1, int stockCount=11)
        {
            Name = name;
            Cost = cost;
            StockCount = stockCount;
        }
    }
}
