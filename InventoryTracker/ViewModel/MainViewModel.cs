using GalaSoft.MvvmLight;
using InventoryTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public List<Item> Data { get; set; }
        private ObservableCollection<Item> employees;
        public MainViewModel()
        {
            Data = new List<Item>();
            GetInput(Data);
            Data.Add(new Item());
        }

        private void GetInput(List<Item> data)
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