using MediatR;

namespace AW.Services.Product.Core.Handlers.GetLocations
{
    public class GetLocationsQuery : IRequest<List<Location>>
    {
    }
}
