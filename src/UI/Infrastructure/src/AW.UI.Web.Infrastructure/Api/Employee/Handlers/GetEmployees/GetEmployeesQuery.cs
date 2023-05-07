using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<Employee>>
    {
    }
}
