using AutoMapper;
using AW.Infrastructure.Api.WCF.CustomerService;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using AddCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.AddCustomerAddress;
using AddCustomerContact = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContact;
using AddCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using DeleteCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerAddress;
using DeleteCustomerContact = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using DeleteCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;
using UpdateCustomerAddress = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerAddress;
using UpdateCustomerContact = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomerContact;

namespace AW.Infrastructure.Api.WCF
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Mappings for AddCustomerAddress
            CreateMap<AddCustomerAddress.AddCustomerAddressRequest, AddCustomerAddressRequest>();
            CreateMap<AddCustomerAddress.CustomerAddress, CustomerAddress2>();
            CreateMap<AddCustomerAddress.Address, Address2>();

            //Mappings for AddCustomerContact
            CreateMap<AddCustomerContact.AddCustomerContactRequest, AddCustomerContactRequest>();
            CreateMap<AddCustomerContact.CustomerContact, CustomerContact2>();
            CreateMap<AddCustomerContact.Contact, Contact2>();
            CreateMap<AddCustomerContact.EmailAddress, EmailAddress>();

            //Mappings for AddCustomerContactInfo
            CreateMap<AddCustomerContactInfo.AddCustomerContactInfoRequest, AddCustomerContactInfoRequest>();
            CreateMap<AddCustomerContactInfo.CustomerContactInfo, CustomerContactInfo>();

            //Mappings for DeleteCustomerAddress
            CreateMap<DeleteCustomerAddress.DeleteCustomerAddressRequest, DeleteCustomerAddressRequest>();

            //Mappings for DeleteCustomerContact
            CreateMap<DeleteCustomerContact.DeleteCustomerContactRequest, DeleteCustomerContactRequest>();
            CreateMap<DeleteCustomerContact.Contact, DeleteContact>();

            //Mappings for DeleteCustomerContactInfo
            CreateMap<DeleteCustomerContactInfo.DeleteCustomerContactInfoRequest, DeleteCustomerContactInfoRequest>();
            CreateMap<DeleteCustomerContactInfo.CustomerContactInfo, CustomerContactInfo1>();

            //Mappings for GetCustomers

            CreateMap<GetCustomer.GetCustomerRequest, GetCustomerRequest>();
            CreateMap<GetCustomerResponseGetCustomerResult, GetCustomer.GetCustomerResponse>();
            CreateMap<Customer1, GetCustomer.Customer>();
            CreateMap<Person1, GetCustomer.Person>();
            CreateMap<Store1, GetCustomer.Store>();
            CreateMap<CustomerAddress1, GetCustomer.CustomerAddress>();
            CreateMap<Address1, GetCustomer.Address>();
            CreateMap<StateProvince1, GetCustomer.StateProvince>();
            CreateMap<CountryRegion1, GetCustomer.CountryRegion>();
            CreateMap<CustomerContact1, GetCustomer.CustomerContact>();
            CreateMap<Contact1, GetCustomer.Contact>();
            CreateMap<ContactInfo1, GetCustomer.ContactInfo>()
                .ForMember(m => m.Channel, opt => opt.MapFrom(src => src.ContactInfoChannelType));
            CreateMap<SalesPerson1, GetCustomer.SalesPerson>();
            CreateMap<SalesOrder1, GetCustomer.SalesOrder>();

            //Mappings for ListCustomers

            CreateMap<ListCustomers.ListCustomersRequest, ListCustomersRequest>()
                .ForMember(m => m.CustomerTypeSpecified, opt => opt.MapFrom(src => src.CustomerType.HasValue));

            CreateMap<ListCustomersResponseListCustomersResult, ListCustomers.ListCustomersResponse>();
            CreateMap<Customer, ListCustomers.Customer>();
            CreateMap<Person, ListCustomers.Person>();
            CreateMap<Store, ListCustomers.Store>();
            CreateMap<CustomerAddress, ListCustomers.CustomerAddress>();
            CreateMap<Address, ListCustomers.Address>();
            CreateMap<StateProvince, ListCustomers.StateProvince>();
            CreateMap<CountryRegion, ListCustomers.CountryRegion>();
            CreateMap<CustomerContact, ListCustomers.CustomerContact>();
            CreateMap<Contact, ListCustomers.Contact>();
            CreateMap<ContactInfo, ListCustomers.ContactInfo>()
                .ForMember(m => m.Channel, opt => opt.MapFrom(src => src.ContactInfoChannelType));
            CreateMap<SalesPerson, ListCustomers.SalesPerson>();
            CreateMap<SalesOrder, ListCustomers.SalesOrder>();

            //Mappings for UpdateCustomer

            CreateMap<UpdateCustomer.UpdateCustomerRequest, UpdateCustomerRequest>();
            CreateMap<UpdateCustomer.Customer, CustomerService.UpdateCustomer>();
            CreateMap<UpdateCustomer.Person, UpdatePerson>();
            CreateMap<UpdateCustomer.Store, UpdateStore>();
            CreateMap<UpdateCustomer.SalesPerson, UpdateSalesPerson>();

            //Mappings for UpdateCustomerAddress

            CreateMap<UpdateCustomerAddress.UpdateCustomerAddressRequest, UpdateCustomerAddressRequest>();
            CreateMap<UpdateCustomerAddress.CustomerAddress, CustomerAddress3>();
            CreateMap<UpdateCustomerAddress.Address, Address3>();

            //Mappings for UpdateCustomerContact

            CreateMap<UpdateCustomerContact.UpdateCustomerContactRequest, UpdateCustomerContactRequest>();
            CreateMap<UpdateCustomerContact.CustomerContact, CustomerContact3>();
            CreateMap<UpdateCustomerContact.Contact, Contact3>();
            CreateMap<UpdateCustomerContact.EmailAddress, EmailAddress1>()
                .ForMember(m => m.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress1));
        }
    }
}