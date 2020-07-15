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
        public string Currency { get; set; }
        private ObservableCollection<Item> _items;
        public List<Item> Items 
        { 
            get 
            {
                return _items.ToList();
            }
        }
        public ICommand OpenAddItemMenuCommand { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MainViewModel()
        {
            Messenger.Default.Register<Tuple<Item, AddItem>>(this, SaveNewItemMethod);
            _items = new ObservableCollection<Item>();
            GetInput(_items);
            Currency = "Cost (Euro)";
            OpenAddItemMenuCommand = new RelayCommand(OpenAddItemMenuMethod);
        }
        private void SaveNewItemMethod(Tuple<Item, AddItem> obj)
        {
            Item item = obj.Item1;
            AddItem view = obj.Item2;
            if(item!=null)
            {
                _items.Add(item);
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