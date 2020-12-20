using Ardalis.Specification;
using AW.Core.Application.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.ContactType.ListContactTypes
{
    public class ListContactTypesQueryHandler : IRequestHandler<ListContactTypesQuery, IEnumerable<string>>
    {
        private readonly IRepositoryBase<Domain.Person.ContactType> repository;

        public ListContactTypesQueryHandler(IRepositoryBase<Domain.Person.ContactType> repository) =>
            this.repository = repository;

        public async Task<IEnumerable<string>> Handle(ListContactTypesQuery request, CancellationToken cancellationToken)
        {
            var contactTypes = await repository.ListAsync();
            if (contactTypes.Count == 0)
                throw new ContactTypesNotFoundException();

            return contactTypes.Select(at => at.Name);
        }
    }
}