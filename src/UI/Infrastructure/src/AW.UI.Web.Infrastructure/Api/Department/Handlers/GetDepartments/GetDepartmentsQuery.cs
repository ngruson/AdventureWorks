using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
    }
}
