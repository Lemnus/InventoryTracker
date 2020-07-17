using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryTracker.Model
{
    public class ItemWithDollar:Item // Not Liskov?
    {
        protected double _costInDollar;

        public ItemWithDollar(string name = "Test", int cost = 1, int stockCount = 11, double costDollar = 0) : base(name, cost, stockCount)
        {
            _costInDollar = costDollar;
        }

        public double CostInDollar
        {
            get
            {
                return _costInDollar;
            }
            set
            {
                Set<double>(() => this.CostInDollar, ref _costInDollar, value);
            }
        }
    }
}
