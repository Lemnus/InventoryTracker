using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using InventoryTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace InventoryTracker.ViewModel
{
    public class AddItemViewModel : ViewModelBase
    {   
        public Item Item { get; set; }

        public ICommand SaveItemCommand { get; private set; }
        public AddItemViewModel()
        {
            SaveItemCommand = new RelayCommand<AddItem>(SaveItemMethod);
            Item = new Item();
        }
        private void SaveItemMethod(AddItem view)
        {
            Tuple<Item, AddItem> ret = new Tuple<Item, AddItem>(Item, view);
            Messenger.Default.Send<Tuple<Item, AddItem>>(ret);
        }
    }
}