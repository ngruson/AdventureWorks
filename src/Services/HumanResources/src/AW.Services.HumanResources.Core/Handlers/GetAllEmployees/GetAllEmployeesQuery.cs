using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<Employee>>
    {

    }
}