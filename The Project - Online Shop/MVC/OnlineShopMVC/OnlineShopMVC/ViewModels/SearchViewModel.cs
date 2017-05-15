using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class SearchViewModel<T> where T:class
    {
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }
        public List<T> SearchItems;
        public SearchViewModel(List<T> SearchItems)
        {
            this.SearchItems = SearchItems;
        }
    }
}