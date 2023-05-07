using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Employee.Handlers.GetJobTitles
{
    public class GetJobTitlesQuery : IRequest<List<string>>
    {
    }
}
