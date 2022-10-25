using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployee
{
    public class GetEmployeeQuery : IRequest<Employee>
    {
        public string? LoginID { get; init; }
    }
}