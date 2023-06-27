using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;

public class GetContactTypesQuery : IRequest<Result<List<ContactType>>>
{
}
