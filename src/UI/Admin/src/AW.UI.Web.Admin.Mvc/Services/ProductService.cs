using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(
            ILogger<ProductService> logger,
            IMapper mapper,
            IMediator mediator
        )
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ProductIndexViewModel> GetProducts(int pageIndex, int pageSize)
        {
            _logger.LogInformation("GetProducts called");
            var response = await _mediator.Send(new GetProductsQuery(
                    pageIndex,
                    pageSize,
                    null, 
                    null
                )
            );

            var totalPages = int.Parse(Math.Ceiling((decimal)response.TotalProducts / pageSize).ToString());

            var vm = new ProductIndexViewModel
            {
                Products = _mapper.Map<List<ProductViewModel>>(response.Products),
                PaginationInfo = new PaginationInfoViewModel(
                    response.TotalProducts,
                    response.Products!.Count,
                    pageIndex,
                    totalPages,
                    pageIndex == 0 ? "disabled" : "",
                    pageIndex == totalPages - 1 ? "disabled" : ""
                )
            };

            return vm;
        }
    }
}
