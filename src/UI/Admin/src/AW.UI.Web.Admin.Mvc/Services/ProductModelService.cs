using AutoMapper;
using AW.UI.Web.Admin.Mvc.ViewModels.ProductModel;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModel;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class ProductModelService : IProductModelService
    {
        private readonly ILogger<ProductModelService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductModelService(ILogger<ProductModelService> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<List<ProductModelViewModel>> GetProductModels()
        {
            _logger.LogInformation("Getting product models");

            var response = await _mediator.Send(new GetProductModelsQuery());            
            var vm = _mapper.Map<List<ProductModelViewModel>>(response);

            _logger.LogInformation("Returning product models");
            return vm;
        }

        public async Task<ProductModelViewModel> GetProductModel(string name)
        {
            _logger.LogInformation("Getting product model");

            var response = await _mediator.Send(new GetProductModelQuery(name));
            var vm = _mapper.Map<ProductModelViewModel>(response);

            _logger.LogInformation("Returning product model");
            return vm;
        }
    }
}
