using MediatR;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public UpdateEmployeeCommand(string key, Employee employee)
        {
            Key = key;
            Employee = employee;
        }

        public string Key { get; set; }
        public Employee Employee { get; set; }
    }
}
