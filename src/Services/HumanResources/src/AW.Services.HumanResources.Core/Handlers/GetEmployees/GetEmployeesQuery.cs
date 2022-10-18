using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesQuery : IRequest<GetEmployeesResult?>
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }
}