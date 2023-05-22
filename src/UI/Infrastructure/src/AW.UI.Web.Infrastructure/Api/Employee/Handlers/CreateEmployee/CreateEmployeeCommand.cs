using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;   
        }

        public Employee Employee { get; set; }
    }
}
