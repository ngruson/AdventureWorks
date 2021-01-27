using Ardalis.Specification;
using AW.Core.Application.Customer.GetCustomers;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using AW.Core.Domain.Person;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetCustomersQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_Page0_ReturnFirstPageOfCustomers()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customers = new List<Domain.Sales.Customer>
            {
                #region Customer 1
                new CustomerBuilder()
                    .AccountNumber("AW00000001")
                    .Store(new StoreBuilder()
                        .Name("A Bike Store")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("2251 Elliot Avenue")
                                        .City("Seattle")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Orlando")
                                        .MiddleName("N.")
                                        .LastName("Gee")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Order Administrator")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Orlando")
                                        .MiddleName("N.")
                                        .LastName("Gee")
                                        .Build()
                                )
                                .Build(),
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Northwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 2
                new CustomerBuilder()
                    .AccountNumber("AW00000002")
                    .Store(new StoreBuilder()
                        .Name("Progressive Sports")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("3207 S Grady Way")
                                        .City("Renton")
                                        .PostalCode("98055")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build(),

                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Shipping")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("7943 Walnut Ave")
                                        .City("Renton")
                                        .PostalCode("98055")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Geraldine")
                                        .MiddleName("T.")
                                        .LastName("Spicer")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Keith")
                                        .LastName("Harris")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Northwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 3
                new CustomerBuilder()
                    .AccountNumber("AW00000003")
                    .Store(new StoreBuilder()
                        .Name("Advanced Bike Components")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Jillian")
                            .LastName("Carson")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("12345 Sterling Avenue")
                                        .City("Irving")
                                        .PostalCode("75061")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Agent")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Donna")
                                        .MiddleName("F.")
                                        .LastName("Carreras")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Joanna")
                                        .MiddleName("B.")
                                        .LastName("Wall")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 4
                new CustomerBuilder()
                    .AccountNumber("AW00000004")
                    .Store(new StoreBuilder()
                        .Name("Modular Cycle Systems")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Jillian")
                            .LastName("Carson")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("800 Interchange Blvd.")
                                        .AddressLine2("Suite 2501")
                                        .City("Austin")
                                        .PostalCode("78701")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build(),

                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Shipping")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("165 North Main")
                                        .AddressLine2("Suite 2501")
                                        .City("Austin")
                                        .PostalCode("78701")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Janet")
                                        .MiddleName("M.")
                                        .LastName("Gates")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 5
                new CustomerBuilder()
                    .AccountNumber("AW00000005")
                    .Store(new StoreBuilder()
                        .Name("Metropolitan Sports Supply")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Shu")
                            .MiddleName("K")
                            .LastName("Ito")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("482505 Warm Springs Blvd.")
                                        .City("Fremont")
                                        .PostalCode("94536")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("CA")
                                            .Name("California")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Agent")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Kristin")
                                        .MiddleName("R.")
                                        .LastName("Spanaway")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Lucy")
                                        .LastName("Harrington")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 6
                new CustomerBuilder()
                    .AccountNumber("AW00011000")
                    .Person(new PersonBuilder()
                        .FirstName("Jon")
                        .MiddleName("V")
                        .LastName("Yang")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("3761 N. 14th St")
                                        .City("Rockhampton")
                                        .PostalCode("4700")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("QLD")
                                            .Name("Queensland")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("jon24@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0162")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 7
                new CustomerBuilder()
                    .AccountNumber("AW00011001")
                    .Person(new PersonBuilder()
                        .FirstName("Eugene")
                        .MiddleName("L")
                        .LastName("Huang")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("2243 W St.")
                                        .City("Seaford")
                                        .PostalCode("3198")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("VIC")
                                            .Name("Victoria")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("eugene10@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0110")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 8
                new CustomerBuilder()
                    .AccountNumber("AW00011002")
                    .Person(new PersonBuilder()
                        .FirstName("Ruben")                        
                        .LastName("Torres")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("5844 Linden Land")
                                        .City("Hobart")
                                        .PostalCode("7001")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TAS")
                                            .Name("Tasmania")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("ruben35@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0184")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 9
                new CustomerBuilder()
                    .AccountNumber("AW00011003")
                    .Person(new PersonBuilder()
                        .FirstName("Christy")
                        .LastName("Zhu")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("1825 Village Pl.")
                                        .City("North Ryde")
                                        .PostalCode("2113")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("NSW")
                                            .Name("New South Wales")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("christy12@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0162")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 10
                new CustomerBuilder()
                    .AccountNumber("AW00011004")
                    .Person(new PersonBuilder()
                        .FirstName("Elizabeth")
                        .LastName("Johnson")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("7553 Harness Circle")
                                        .City("Wollongong")
                                        .PostalCode("2500")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("NSW")
                                            .Name("New South Wales")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("elizabeth5@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0131")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build()
                #endregion
            };

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.ListAsync(It.IsAny<GetCustomersPaginatedSpecification>()))
                .ReturnsAsync(customers.Take(5).ToList());
            customerRepoMock.Setup(x => x.CountAsync(It.IsAny<CountCustomersSpecification>()))
                .ReturnsAsync(10);

            var handler = new GetCustomersQueryHandler(
                customerRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCustomersQuery
            {
                PageIndex = 0,
                PageSize = 5
            };

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Customers.Count().Should().Be(5);
            result.TotalCustomers.Should().Be(10);
        }

        [Fact]
        public async void Handle_Page1_ReturnSecondPageOfCustomers()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customers = new List<Domain.Sales.Customer>
            {
                #region Customer 1
                new CustomerBuilder()
                    .AccountNumber("AW00000001")
                    .Store(new StoreBuilder()
                        .Name("A Bike Store")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("2251 Elliot Avenue")
                                        .City("Seattle")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Orlando")
                                        .MiddleName("N.")
                                        .LastName("Gee")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Order Administrator")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Orlando")
                                        .MiddleName("N.")
                                        .LastName("Gee")
                                        .Build()
                                )
                                .Build(),
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Northwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 2
                new CustomerBuilder()
                    .AccountNumber("AW00000002")
                    .Store(new StoreBuilder()
                        .Name("Progressive Sports")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("3207 S Grady Way")
                                        .City("Renton")
                                        .PostalCode("98055")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build(),

                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Shipping")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("7943 Walnut Ave")
                                        .City("Renton")
                                        .PostalCode("98055")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("WA")
                                            .Name("Washington")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Geraldine")
                                        .MiddleName("T.")
                                        .LastName("Spicer")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Keith")
                                        .LastName("Harris")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Northwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 3
                new CustomerBuilder()
                    .AccountNumber("AW00000003")
                    .Store(new StoreBuilder()
                        .Name("Advanced Bike Components")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Jillian")
                            .LastName("Carson")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("12345 Sterling Avenue")
                                        .City("Irving")
                                        .PostalCode("75061")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Agent")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Donna")
                                        .MiddleName("F.")
                                        .LastName("Carreras")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Joanna")
                                        .MiddleName("B.")
                                        .LastName("Wall")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 4
                new CustomerBuilder()
                    .AccountNumber("AW00000004")
                    .Store(new StoreBuilder()
                        .Name("Modular Cycle Systems")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Jillian")
                            .LastName("Carson")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("800 Interchange Blvd.")
                                        .AddressLine2("Suite 2501")
                                        .City("Austin")
                                        .PostalCode("78701")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build(),

                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Shipping")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("165 North Main")
                                        .AddressLine2("Suite 2501")
                                        .City("Austin")
                                        .PostalCode("78701")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TX")
                                            .Name("Texas")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Owner")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Janet")
                                        .MiddleName("M.")
                                        .LastName("Gates")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 5
                new CustomerBuilder()
                    .AccountNumber("AW00000005")
                    .Store(new StoreBuilder()
                        .Name("Metropolitan Sports Supply")
                        .SalesPerson(new SalesPersonBuilder()
                            .FirstName("Shu")
                            .MiddleName("K")
                            .LastName("Ito")
                            .Build()
                        )
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Main Office")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("482505 Warm Springs Blvd.")
                                        .City("Fremont")
                                        .PostalCode("94536")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("CA")
                                            .Name("California")
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .Contacts(new List<BusinessEntityContact>
                            {
                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Agent")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Ms.")
                                        .FirstName("Kristin")
                                        .MiddleName("R.")
                                        .LastName("Spanaway")
                                        .Build()
                                )
                                .Build(),

                                new BusinessEntityContactBuilder()
                                    .ContactType(new ContactTypeBuilder()
                                        .Name("Purchasing Manager")
                                        .Build()
                                    )
                                    .Person(new PersonBuilder()
                                        .Title("Mr.")
                                        .FirstName("Lucy")
                                        .LastName("Harrington")
                                        .Build()
                                )
                                .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Southwest")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("US")
                            .Name("United States")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 6
                new CustomerBuilder()
                    .AccountNumber("AW00011000")
                    .Person(new PersonBuilder()
                        .FirstName("Jon")
                        .MiddleName("V")
                        .LastName("Yang")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("3761 N. 14th St")
                                        .City("Rockhampton")
                                        .PostalCode("4700")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("QLD")
                                            .Name("Queensland")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("jon24@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0162")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 7
                new CustomerBuilder()
                    .AccountNumber("AW00011001")
                    .Person(new PersonBuilder()
                        .FirstName("Eugene")
                        .MiddleName("L")
                        .LastName("Huang")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("2243 W St.")
                                        .City("Seaford")
                                        .PostalCode("3198")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("VIC")
                                            .Name("Victoria")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("eugene10@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0110")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 8
                new CustomerBuilder()
                    .AccountNumber("AW00011002")
                    .Person(new PersonBuilder()
                        .FirstName("Ruben")
                        .LastName("Torres")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("5844 Linden Land")
                                        .City("Hobart")
                                        .PostalCode("7001")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("TAS")
                                            .Name("Tasmania")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("ruben35@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0184")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 9
                new CustomerBuilder()
                    .AccountNumber("AW00011003")
                    .Person(new PersonBuilder()
                        .FirstName("Christy")
                        .LastName("Zhu")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("1825 Village Pl.")
                                        .City("North Ryde")
                                        .PostalCode("2113")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("NSW")
                                            .Name("New South Wales")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("christy12@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0162")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build(),
                #endregion

                #region Customer 10
                new CustomerBuilder()
                    .AccountNumber("AW00011004")
                    .Person(new PersonBuilder()
                        .FirstName("Elizabeth")
                        .LastName("Johnson")
                        .Addresses(new List<BusinessEntityAddress>
                            {
                                new BusinessEntityAddressBuilder()
                                    .AddressType(new AddressTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .Address(new AddressBuilder()
                                        .AddressLine1("7553 Harness Circle")
                                        .City("Wollongong")
                                        .PostalCode("2500")
                                        .StateProvince(new StateProvinceBuilder()
                                            .StateProvinceCode("NSW")
                                            .Name("New South Wales")
                                            .CountryRegion(new CountryRegionBuilder()
                                                .CountryRegionCode("AU")
                                                .Name("Australia")
                                                .Build()
                                            )
                                            .Build()
                                        )
                                        .Build()
                                    )
                                    .Build()
                            }
                        )
                        .EmailAddresses(new List<EmailAddress>
                            {
                                new EmailAddressBuilder()
                                    .EmailAddress1("elizabeth5@adventure-works.com")
                                    .Build()
                            }
                        )
                        .PhoneNumbers(new List<PersonPhone>
                            {
                                new PersonPhoneBuilder()
                                    .PhoneNumberType(new PhoneNumberTypeBuilder()
                                        .Name("Home")
                                        .Build()
                                    )
                                    .PhoneNumber("1 (11) 500 555-0131")
                                    .Build()
                            }
                        )
                        .Build()
                    )
                    .SalesTerritory(new SalesTerritoryBuilder()
                        .Name("Australia")
                        .CountryRegion(new CountryRegionBuilder()
                            .CountryRegionCode("AU")
                            .Name("Australia")
                            .Build()
                        )
                        .Build()
                    )
                    .Build()
                #endregion
            };

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.ListAsync(It.IsAny<GetCustomersPaginatedSpecification>()))
                .ReturnsAsync(customers.Skip(5).Take(5).ToList());
            customerRepoMock.Setup(x => x.CountAsync(It.IsAny<CountCustomersSpecification>()))
                .ReturnsAsync(10);

            var handler = new GetCustomersQueryHandler(
                customerRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCustomersQuery
            {
                PageIndex = 1,
                PageSize = 5
            };

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Customers.Count().Should().Be(5);
            result.TotalCustomers.Should().Be(10);
        }
    }
}