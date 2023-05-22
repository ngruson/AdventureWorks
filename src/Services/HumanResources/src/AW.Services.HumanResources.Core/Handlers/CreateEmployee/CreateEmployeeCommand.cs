using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<Result<CreatedEmployee>>
    {
        public CreateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }
        public Employee Employee { get; set; }
    }
}
