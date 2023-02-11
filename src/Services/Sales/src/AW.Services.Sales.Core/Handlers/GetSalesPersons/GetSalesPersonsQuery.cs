using MediatR;

namespace AW.Services.Sales.Core.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<List<SalesPersonDto>?>
    {
        public GetSalesPersonsQuery(string? territory)
        {
            Territory = territory;
        }

        public string? Territory { get; private init; }
    }
}