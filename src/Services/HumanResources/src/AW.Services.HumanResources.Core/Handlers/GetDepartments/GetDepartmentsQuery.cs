using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<Result<List<Department>>>
    {
    }
}
