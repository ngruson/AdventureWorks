using AutoMapper;
using AW.Application.Customer.GetCustomers;
using AW.Services.API.CustomerAPI.Models.ListCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AW.Services.API.CustomerAPI.Controllers
{
    [ApiController]
    [Route("/api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerController(IMediator mediator, IMapper mapper) =>
            (this.mediator, this.mapper) = (mediator, mapper);

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] GetCustomersQuery request)
        {
            //var query = new GetCustomersQuery
            //{
            //    PageIndex = request.PageIndex,
            //    PageSize = request.PageSize,
            //    CustomerType = request.CustomerType,
            //    Territory = request.Territory
            //};
            var customers = await mediator.Send(request);

            if (customers.Customers == null || customers.Customers.Count() == 0)
                return new NotFoundResult();

            var response = mapper.Map<ListCustomersResponse>(customers);
            return new OkObjectResult(response);
        }
    }
}