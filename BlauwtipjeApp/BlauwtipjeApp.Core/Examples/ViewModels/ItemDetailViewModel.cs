﻿using BlauwtipjeApp.Core.Examples.Models;

namespace BlauwtipjeApp.Core.Examples.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            if (item != null)
            {
                Title = item.Text;
                Item = item;
            }
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}