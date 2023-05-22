using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetEmployees
{
    public class GetEmployeesQuery : IRequest<Result<List<Employee>>>
    {
    }
}
