using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InventoryTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Item> _items;
        public string Currency { get; set; }
        public List<Item> Items 
        { get 
            {
                return _items.ToList();
            } 
        }
        public ICommand AddItemCommand { get; private set; }
        public MainViewModel()
        {
            _items = new ObservableCollection<Item>();
            GetInput(_items);
            Currency = "Cost (Euro)";
            AddItemCommand = new RelayCommand(AddItemMethod);
        }

        private void AddItemMethod()
        {
            AddItem win2 = new AddItem();
            win2.Show();
        }

        private void GetInput(ObservableCollection<Item> data)
        {            
            string line;
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\InventoryCSV.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] items = line.Split(',');
                for (int i = 0; i < 3; i++)
                    items[i]= items[i].TrimStart();
                data.Add(new Item(items[0],
                                  int.Parse(items[1]),
                                  int.Parse(items[2])));
            }
            file.Close();            
        }
    }
}