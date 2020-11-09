using AW.Application.Interfaces;
using AW.Application.Specifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.DeleteCustomerContact
{
    public class DeleteCustomerContactCommandHandler : IRequestHandler<DeleteCustomerContactCommand, Unit>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;

        public DeleteCustomerContactCommandHandler(IAsyncRepository<Domain.Sales.Customer> customerRepository) =>
            this.customerRepository = customerRepository;

        public async Task<Unit> Handle(DeleteCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(
                new GetCustomerSpecification(request.AccountNumber)
            );

            var customerContact = customer.Store.BusinessEntityContacts.FirstOrDefault(c =>
                c.ContactType.Name == request.ContactTypeName &&
                c.Person.FirstName == request.Contact.FirstName &&
                c.Person.MiddleName == request.Contact.MiddleName &&
                c.Person.LastName == request.Contact.LastName
            );

            customer.Store.BusinessEntityContacts.Remove(customerContact);

            await customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}