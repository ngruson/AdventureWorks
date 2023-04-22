using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetJobTitles
{
    public class GetJobTitlesQuery : IRequest<List<string>>
    {
    }
}
