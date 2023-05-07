using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Department.Handlers.GetDepartment
{
    public class GetDepartmentQuery : IRequest<Department?>
    {
        public GetDepartmentQuery(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
