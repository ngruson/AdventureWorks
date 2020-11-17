using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Domain.Person;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, Unit>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;

        public DeleteCustomerAddressCommandHandler(IRepositoryBase<Domain.Sales.Customer> customerRepository) =>
            this.customerRepository = customerRepository;

        public async Task<Unit> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            ICollection<BusinessEntityAddress> customerAddresses = null;
            if (customer.Store != null)
                customerAddresses = customer.Store.BusinessEntityAddresses;
            else if (customer.Person != null)
                customerAddresses = customer.Person.BusinessEntityAddresses;

            var customerAddress = customerAddresses.FirstOrDefault(
                a => a.AddressType.Name == request.AddressTypeName
            );

            customerAddresses.Remove(customerAddress);

            await customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}