using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<Result<UpdatedEmployee>>
    {
        public UpdateEmployeeCommand(Employee employee)
        {
            Employee = employee;
        }

        public Employee Employee { get; set; }
    }
}
