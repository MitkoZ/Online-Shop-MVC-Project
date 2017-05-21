using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class AllItemsSearchEngineViewModel
    {
        public List<PCsViewModel> computersViewModel;
        public List<SmartphonesViewModel> smartphonesViewModel;
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }
        public AllItemsSearchEngineViewModel(List<PCsViewModel> computersViewModel, List<SmartphonesViewModel> smartphonesViewModel)
        {
            this.computersViewModel = computersViewModel;
            this.smartphonesViewModel = smartphonesViewModel;
        }
        
    }
}