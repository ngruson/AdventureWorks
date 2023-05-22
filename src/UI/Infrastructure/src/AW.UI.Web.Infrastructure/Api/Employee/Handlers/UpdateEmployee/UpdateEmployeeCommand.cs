using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<UpdatedEmployee>
    {
        public UpdateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; set; }
    }
}
