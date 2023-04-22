using MediatR;

namespace AW.UI.Web.SharedKernel.Department.Handlers.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<Department>>
    {
    }
}
