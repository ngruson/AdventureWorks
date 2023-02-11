namespace AW.UI.Web.Store.ViewModels
{
    public class PaginationInfoViewModel
    {
        public PaginationInfoViewModel(int totalItems, int itemsPerPage, int actualPage, int totalPages, string previous, string next)
        {
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            ActualPage = actualPage;
            TotalPages = totalPages;
            Previous = previous;
            Next = next;
        }

        public int TotalItems { get; private init; }
        public int ItemsPerPage { get; private init; }
        public int ActualPage { get; private init; }
        public int TotalPages { get; private init; }
        public string Previous { get; private init; }
        public string Next { get; private init; }
    }
}