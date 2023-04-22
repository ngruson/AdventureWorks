using MediatR;

namespace AW.UI.Web.SharedKernel.Employee.Handlers.GetJobTitles
{
    public class GetJobTitlesQuery : IRequest<List<string>>
    {
    }
}
