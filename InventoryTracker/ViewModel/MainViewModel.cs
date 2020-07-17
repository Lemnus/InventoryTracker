using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using InventoryTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Navigation;

namespace InventoryTracker.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {     
        private double exchangeRate = 1.1;
        private ObservableCollection<ItemWithDollar> _itemsWithDollars;        
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OpenAddItemMenuCommand { get; private set; }
        public string Currency { get; set; }
        public string Currency2 { get; set; }
        public List<ItemWithDollar> Items
        { 
            get 
            {
                return _itemsWithDollars.ToList();
            }
        }        
        public MainViewModel()
        {
            Messenger.Default.Register<Tuple<Item, AddItem>>(this, SaveNewItemMethod);
            _itemsWithDollars = new ObservableCollection<ItemWithDollar>();
            GetInput(_itemsWithDollars);
            Currency = "Cost (Euro)";
            Currency2 = "Cost (Dollar)";
            OpenAddItemMenuCommand = new RelayCommand(OpenAddItemMenuMethod);
        }
        private void SaveNewItemMethod(Tuple<Item, AddItem> obj)
        {
            Item item = obj.Item1;
            AddItem view = obj.Item2;
            if (item != null)
            {
                ItemWithDollar itemWithDollar =
                new ItemWithDollar
                {
                    Cost = item.Cost,
                    Name = item.Name,
                    CostInDollar = item.Cost * exchangeRate,
                    StockCount = item.StockCount
                };    
                _itemsWithDollars.Add(itemWithDollar);
                OnNotifyPropertyChanged("Items");
            }
            view.Close();
        }
        protected void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OpenAddItemMenuMethod()
        {
            AddItem win2 = new AddItem();
            win2.Show();
        }
        private void GetInput(ObservableCollection<ItemWithDollar> data)
        {            
            string line;
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\InventoryCSV.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                //Name, Cost, Stock (, CostInDollars)
                for (int i = 0; i < 3; i++)
                    items[i]= items[i].TrimStart();
                data.Add(new ItemWithDollar(items[0],
                                  int.Parse(items[1]),                                  
                                  int.Parse(items[2]),
                                  double.Parse(items[1]) * exchangeRate));
            }
            file.Close();            
        }
    }
}