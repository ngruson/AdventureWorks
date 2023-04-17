using MediatR;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<Employee>>
    {
    }
}
