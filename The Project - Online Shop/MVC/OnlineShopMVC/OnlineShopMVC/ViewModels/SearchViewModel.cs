using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopMVC.ViewModels
{
    public class SearchViewModel<T> where T:ISearchItem
    {
        public string LastSortColumn { get; set; }
        public string LastSortDirection { get; set; }
        public List<T> SearchItems;

        public int PageSize { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TotalPagesCount { get; set; }
        public int TotalItemsCount { get; set; }
        public bool HasFirstPage
        {
            get { return CurrentPageIndex > 1; }
        }
        public bool HasLastPage
        {
            get { return CurrentPageIndex < TotalPagesCount; }
        }
        public bool HasPrevPage
        {
            get { return CurrentPageIndex > 1; }
        }
        public bool HasNextPage
        {
            get { return CurrentPageIndex < TotalPagesCount; }
        }

        public SearchViewModel(List<T> searchItems, int pageSize, int pageIndex, int recordsCount, string lastSortColumn, string lastSortDirection)
        {
            this.PageSize = pageSize;
            this.SearchItems = searchItems;
            this.LastSortColumn = lastSortColumn;
            this.LastSortDirection = lastSortDirection;

            PageSize = pageSize;
            TotalItemsCount = recordsCount;
            TotalPagesCount = ((recordsCount - 1) / PageSize) + 1;
            CurrentPageIndex = pageIndex;
        }
    }
}