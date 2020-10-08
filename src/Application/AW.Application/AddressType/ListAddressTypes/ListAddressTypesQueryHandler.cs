using AW.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.AddressType.ListAddressTypes
{
    public class ListAddressTypesQueryHandler : IRequestHandler<ListAddressTypesQuery, IEnumerable<string>>
    {
        private readonly IAsyncRepository<Domain.Person.AddressType> repository;

        public ListAddressTypesQueryHandler(IAsyncRepository<Domain.Person.AddressType> repository) =>
            this.repository = repository;

        public async Task<IEnumerable<string>> Handle(ListAddressTypesQuery request, CancellationToken cancellationToken)
        {
            var addressTypes = await repository.ListAllAsync();
            return addressTypes.Select(at => at.Name);
                
        }
    }
}