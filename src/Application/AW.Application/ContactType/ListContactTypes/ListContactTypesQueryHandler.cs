using AW.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.ContactType.ListContactTypes
{
    public class ListContactTypesQueryHandler : IRequestHandler<ListContactTypesQuery, IEnumerable<string>>
    {
        private readonly IAsyncRepository<Domain.Person.ContactType> repository;

        public ListContactTypesQueryHandler(IAsyncRepository<Domain.Person.ContactType> repository) =>
            this.repository = repository;

        public async Task<IEnumerable<string>> Handle(ListContactTypesQuery request, CancellationToken cancellationToken)
        {
            var contactTypes = await repository.ListAllAsync();
            return contactTypes.Select(at => at.Name);
        }
    }
}