﻿using AutoMapper;
using AW.Services.Customer.Core.Handlers.AddCustomerAddress;
using AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Handlers.AddStoreCustomerContact;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.AddCustomerAddress;
using AW.Services.Customer.WCF.Messages.AddIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.AddStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.DeleteCustomerAddress;
using AW.Services.Customer.WCF.Messages.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.WCF.Messages.DeleteStoreCustomerContact;
using AW.Services.Customer.WCF.Messages.GetCustomer;
using AW.Services.Customer.WCF.Messages.ListCustomers;
using AW.Services.Customer.WCF.Messages.UpdateCustomer;
using AW.Services.Customer.WCF.Messages.UpdateCustomerAddress;
using AW.Services.Customer.WCF.Messages.UpdateStoreCustomerContact;
using MediatR;
using System.Threading.Tasks;

namespace AW.Services.Customer.WCF
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerService(IMediator mediator, IMapper mapper)
            => (this.mediator, this.mapper) = (mediator, mapper);

        public async Task<ListCustomersResponse> ListCustomers(ListCustomersRequest request)
        {
            var query = new GetCustomersQuery
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                CustomerType = mapper.Map<CustomerType>(request.CustomerType),
                Territory = request.Territory
            };
            var customers = await mediator.Send(query);

            var response = mapper.Map<ListCustomersResponse>(customers);
            return response;
        }

        public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)
        {
            var customer = await mediator.Send(
                new GetCustomerQuery { AccountNumber = request.AccountNumber }
            );

            var response = new GetCustomerResponse
            {
                Customer = mapper.Map<Core.Models.GetCustomer.Customer>(customer)
            };

            return response;
        }

        public async Task<UpdateCustomerResponse> UpdateCustomer(UpdateCustomerRequest request)
        {
            var customer = await mediator.Send(
                new UpdateCustomerCommand
                {
                    Customer = mapper.Map<Core.Handlers.UpdateCustomer.CustomerDto>(request.Customer)
                }
            );

            return new UpdateCustomerResponse
            {
                Customer = mapper.Map<Core.Models.UpdateCustomer.Customer>(customer)
            };
        }

        public async Task<AddCustomerAddressResponse> AddCustomerAddress(AddCustomerAddressRequest request)
        {
            var command = mapper.Map<AddCustomerAddressCommand>(request);
            await mediator.Send(command);

            return new AddCustomerAddressResponse();
        }

        public async Task<UpdateCustomerAddressResponse> UpdateCustomerAddress(UpdateCustomerAddressRequest request)
        {
            var command = mapper.Map<UpdateCustomerAddressCommand>(request);
            await mediator.Send(command);

            return new UpdateCustomerAddressResponse();
        }

        public async Task<DeleteCustomerAddressResponse> DeleteCustomerAddress(DeleteCustomerAddressRequest request)
        {
            var command = mapper.Map<DeleteCustomerAddressCommand>(request);
            await mediator.Send(command);

            return new DeleteCustomerAddressResponse();
        }

        public async Task<AddStoreCustomerContactResponse> AddStoreCustomerContact(AddStoreCustomerContactRequest request)
        {
            var command = mapper.Map<AddStoreCustomerContactCommand>(request);
            await mediator.Send(command);

            return new AddStoreCustomerContactResponse();
        }

        public async Task<UpdateStoreCustomerContactResponse> UpdateStoreCustomerContact(UpdateStoreCustomerContactRequest request)
        {
            var command = mapper.Map<UpdateStoreCustomerContactCommand>(request);
            await mediator.Send(command);

            return new UpdateStoreCustomerContactResponse();
        }

        public async Task<DeleteStoreCustomerContactResponse> DeleteStoreCustomerContact(DeleteStoreCustomerContactRequest request)
        {
            var command = mapper.Map<DeleteStoreCustomerContactCommand>(request);
            await mediator.Send(command);

            return new DeleteStoreCustomerContactResponse();
        }

        public async Task<AddIndividualCustomerEmailAddressResponse> AddIndividualCustomerEmailAddress(AddIndividualCustomerEmailAddressRequest request)
        {
            var command = mapper.Map<AddIndividualCustomerEmailAddressCommand>(request);
            await mediator.Send(command);

            return new AddIndividualCustomerEmailAddressResponse();
        }

        public async Task<DeleteIndividualCustomerEmailAddressResponse> DeleteIndividualCustomerEmailAddress(DeleteIndividualCustomerEmailAddressRequest request)
        {
            var command = mapper.Map<DeleteIndividualCustomerEmailAddressCommand>(request);
            await mediator.Send(command);

            return new DeleteIndividualCustomerEmailAddressResponse();
        }
    }
}