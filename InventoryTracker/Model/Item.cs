using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Model
{
    public class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int StockCount { get; set; }
        public Item(string name="Test", int cost=1, int stockCount=11)
        {
            Name = name;
            Cost = cost;
            StockCount = stockCount;
        }
    }
}
