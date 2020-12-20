using Ardalis.Specification;
using AW.Core.Application.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Core.Application.AddressType.ListAddressTypes
{
    public class ListAddressTypesQueryHandler : IRequestHandler<ListAddressTypesQuery, IEnumerable<string>>
    {
        private readonly IRepositoryBase<Domain.Person.AddressType> repository;

        public ListAddressTypesQueryHandler(IRepositoryBase<Domain.Person.AddressType> repository) =>
            this.repository = repository;

        public async Task<IEnumerable<string>> Handle(ListAddressTypesQuery request, CancellationToken cancellationToken)
        {
            var addressTypes = await repository.ListAsync();
            if (addressTypes.Count == 0)
                throw new AddressTypesNotFoundException();

            return addressTypes.Select(at => at.Name);
                
        }
    }
}