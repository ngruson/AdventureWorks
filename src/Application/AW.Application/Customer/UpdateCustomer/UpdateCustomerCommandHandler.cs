using AutoMapper;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Domain.Person;
using AW.Domain.Sales;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Sales.SalesTerritory> salesTerritoryRepository;
        private readonly IAsyncRepository<Domain.Sales.SalesPerson> salesPersonRepository;
        private readonly IMapper mapper;

        public UpdateCustomerCommandHandler(
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Domain.Sales.SalesTerritory> salesTerritoryRepository,
            IAsyncRepository<Domain.Sales.SalesPerson> salesPersonRepository,
            IMapper mapper) =>
                (this.customerRepository, this.salesTerritoryRepository, this.salesPersonRepository, this.mapper) =
                (customerRepository, salesTerritoryRepository, salesPersonRepository, mapper);

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var spec = new GetCustomerSpecification(request.Customer.AccountNumber);
            var customer = await customerRepository.FirstOrDefaultAsync(spec);

            await UpdateSalesTerritory(request, customer);
            if (customer.Person != null)
                UpdatePersonCustomer(request, customer.Person);
            else if (customer.Store != null)
                await UpdateStoreCustomer(request, customer.Store);

            await customerRepository.UpdateAsync(customer);

            return mapper.Map<CustomerDto>(customer);
        }

        private async Task UpdateSalesTerritory(UpdateCustomerCommand request, Domain.Sales.Customer customer)
        {
            if (!string.IsNullOrEmpty(request.Customer.SalesTerritoryName))
            {
                var territorySpec = new GetSalesTerritorySpecification(request.Customer.SalesTerritoryName);
                var salesTerritory = await salesTerritoryRepository.FirstOrDefaultAsync(territorySpec);
                customer.SalesTerritoryID = salesTerritory.Id;
            }
            else
                customer.SalesTerritory = null;
        }

        private void UpdatePersonCustomer(UpdateCustomerCommand request, Person person)
        {
            person.Title = request.Customer.Person.Title;
            person.FirstName = request.Customer.Person.FirstName;
            person.MiddleName = request.Customer.Person.MiddleName;
            person.LastName = request.Customer.Person.LastName;
            person.Suffix = request.Customer.Person.Suffix;
            person.EmailPromotion = request.Customer.Person.EmailPromotion;
        }

        private async Task UpdateStoreCustomer(UpdateCustomerCommand request, Store store)
        {
            store.Name = request.Customer.Store.Name;
            if (request.Customer.Store.SalesPerson != null)
            {
                var spec = new GetSalesPersonSpecification(
                    request.Customer.Store.SalesPerson.FirstName,
                    request.Customer.Store.SalesPerson.MiddleName,
                    request.Customer.Store.SalesPerson.LastName
                );

                var salesPerson = await salesPersonRepository.FirstOrDefaultAsync(spec);
                if (salesPerson != null)
                    store.SalesPersonID = salesPerson.Id;
            }
            else
                store.SalesPerson = null;
        }
    }
}