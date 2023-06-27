using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;

public class ContactType : IMapFrom<Entities.ContactType>
{
    public Guid ObjectId { get; private init; }
    public string? Name { get; private init; }
}
