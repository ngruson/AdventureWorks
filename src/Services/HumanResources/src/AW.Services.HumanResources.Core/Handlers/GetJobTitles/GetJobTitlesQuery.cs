using Ardalis.Result;
using MediatR;

namespace AW.Services.HumanResources.Core.Handlers.GetJobTitles
{
    public class GetJobTitlesQuery : IRequest<Result<List<string>>>
    {
    }
}
