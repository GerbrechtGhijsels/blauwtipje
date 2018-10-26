using BlauwtipjeApp.Core.Examples.Helpers;
using BlauwtipjeApp.Core.Examples.Models;
using BlauwtipjeApp.Core.Examples.Services;
using BlauwtipjeApp.Core.Helpers;

namespace BlauwtipjeApp.Core.Examples.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => ServiceLocator.GetService<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}

