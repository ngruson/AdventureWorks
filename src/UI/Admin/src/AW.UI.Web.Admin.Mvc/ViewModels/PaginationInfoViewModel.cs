namespace AW.UI.Web.Admin.Mvc.ViewModels;

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

    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int ActualPage { get; set; }
    public int TotalPages { get; set; }
    public string Previous { get; set; }
    public string Next { get; set; }
}
