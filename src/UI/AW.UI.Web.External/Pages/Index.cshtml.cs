using AW.UI.Web.External.Interfaces;
using AW.UI.Web.External.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AW.UI.Web.External.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IProductsViewModelService productsViewModelService;

        public IndexModel(ILogger<IndexModel> logger, IProductsViewModelService productsViewModelService)
        {
            this.logger = logger;
            this.productsViewModelService = productsViewModelService;
        }

        public ProductsIndexViewModel ProductsModel { get; set; }

        public async Task OnGet(int? pageId)
        {
            ProductsModel = await productsViewModelService.GetProducts(pageId ?? 0, Constants.ITEMS_PER_PAGE);
        }
    }
}