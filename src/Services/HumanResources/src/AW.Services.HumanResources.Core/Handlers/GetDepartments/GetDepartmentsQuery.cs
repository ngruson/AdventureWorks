using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
    }
}