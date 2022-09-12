using RestWithASPNET.HyperMedia.Abstract;
using System.Collections.Generic;

namespace RestWithASPNET.HyperMedia.Utils
{
    public class PagedSarchVO<T> where T : ISupportsHyperMedia
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }
        public Dictionary<string,object> Filters { get; set; }

        public List<T> list { get; set; }

        public PagedSarchVO()
        {
        }

        public PagedSarchVO(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSarchVO(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters) 
        {
            Filters = filters;
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSarchVO(int currentPage, string sortFields, string sortDirections) 
            : this(currentPage,10 , sortFields,sortDirections)
        {
        }
        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }
        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
