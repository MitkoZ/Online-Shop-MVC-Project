using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class AllItemsSearchEngineViewModel
    {
        public List<PCsViewModel> ComputersViewModel;
        public List<SmartphonesViewModel> SmartphonesViewModel;
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }
        public AllItemsSearchEngineViewModel(List<PCsViewModel> computersViewModel, List<SmartphonesViewModel> smartphonesViewModel)
        {
            this.ComputersViewModel = computersViewModel;
            this.SmartphonesViewModel = smartphonesViewModel;
        }
        
    }
}