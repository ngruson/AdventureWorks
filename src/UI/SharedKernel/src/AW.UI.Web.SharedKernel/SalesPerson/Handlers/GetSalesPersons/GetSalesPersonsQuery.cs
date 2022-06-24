using MediatR;

namespace AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons
{
    public class GetSalesPersonsQuery : IRequest<List<SalesPerson>>
    {
        public GetSalesPersonsQuery(string? territory)
        {
            Territory = territory;
        }

        public string? Territory { get; set; }
    }
}