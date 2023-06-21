using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class CustomerConverterUnitTests
    {
        public class GetCustomers
        {
            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomersStore_ReturnsStoreCustomer(
                StoreCustomer storeCustomer,
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, Core.Handlers.GetCustomer.StoreCustomer, Core.Handlers.GetCustomer.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            StoreCustomer,
                            IndividualCustomer>(
                                mockLogger.Object    
                            )
                    }
                };

                //Act
                var jsonString = JsonSerializer.Serialize(storeCustomer, serializeOptions);
                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;
                var contacts = root.GetProperty("contacts").EnumerateArray().ToList();                                
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();

                //Assert
                root.GetProperty("customerType").GetString().Should().Be(storeCustomer.CustomerType.ToString());
                root.GetProperty("name").GetString().Should().Be(storeCustomer.Name);
                root.GetProperty("salesPerson").GetString().Should().Be(storeCustomer.SalesPerson);
                root.GetProperty("accountNumber").GetString().Should().Be(storeCustomer.AccountNumber);
                root.GetProperty("territory").GetString().Should().Be(storeCustomer.Territory);

                for (int i = 0; i < contacts.Count; i++)
                {
                    var contact = contacts[i];
                    var contactPerson = contacts[i].GetProperty("contactPerson");
                    contact.GetProperty("contactType").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactType);
                    contactPerson.GetProperty("title").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Title);
                    contactPerson.GetProperty("name").GetProperty("firstName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.FirstName);
                    contactPerson.GetProperty("name").GetProperty("middleName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.MiddleName);
                    contactPerson.GetProperty("name").GetProperty("lastName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.LastName);
                    contactPerson.GetProperty("suffix").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Suffix);

                    var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();

                    for (int j = 0; j < emailAddresses.Count; j++)
                    {
                        var emailAddress = emailAddresses[j];
                        emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.EmailAddresses![j].EmailAddress);
                    }

                    var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();

                    for (int j = 0; j < phoneNumbers.Count; j++)
                    {
                        var phoneNumber = phoneNumbers[j];
                        phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.PhoneNumbers![j].PhoneNumberType);
                        phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.PhoneNumbers![j].PhoneNumber);
                    }
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        storeCustomer.Addresses![i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomersIndividual_ReturnsIndividualCustomer(
                IndividualCustomer individualCustomer,
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, StoreCustomer, IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            Core.Handlers.GetCustomer.StoreCustomer,
                            Core.Handlers.GetCustomer.IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var jsonString = JsonSerializer.Serialize(individualCustomer, serializeOptions);
                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;
                var person = root.GetProperty("person");
                var emailAddresses = person.GetProperty("emailAddresses").EnumerateArray().ToList();
                var phoneNumbers = person.GetProperty("phoneNumbers").EnumerateArray().ToList();
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Individual");
                root.GetProperty("accountNumber").GetString().Should().Be(individualCustomer.AccountNumber);
                root.GetProperty("territory").GetString().Should().Be(individualCustomer.Territory);

                person.GetProperty("title").GetString().Should().Be(individualCustomer.Person!.Title);
                person.GetProperty("name").GetProperty("firstName").GetString().Should().Be(individualCustomer.Person.Name!.FirstName);
                person.GetProperty("name").GetProperty("middleName").GetString().Should().Be(individualCustomer.Person.Name.MiddleName);
                person.GetProperty("name").GetProperty("lastName").GetString().Should().Be(individualCustomer.Person.Name.LastName);
                person.GetProperty("suffix").GetString().Should().Be(individualCustomer.Person.Suffix);

                for (int i = 0; i < emailAddresses.Count; i++)
                {
                    var emailAddress = emailAddresses[i];
                    emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                        individualCustomer.Person.EmailAddresses![i].EmailAddress);
                }

                for (int i = 0; i < phoneNumbers.Count; i++)
                {
                    var phoneNumber = phoneNumbers[i];
                    phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers![i].PhoneNumberType);
                    phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumber);
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        individualCustomer.Addresses![i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Deserialize_WithValidJson_ReturnsStoreCustomer(
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, Core.Handlers.GetCustomer.StoreCustomer, Core.Handlers.GetCustomer.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var json = @"{
                    ""customerType"": ""Store"",
                    ""name"": ""A Bike Store"",
                    ""salesPerson"": ""Pamela O Ansman-Wolfe"",
                    ""contacts"": [
                      {
                        ""contactType"": ""Owner"",
                        ""contactPerson"": {
                          ""title"": null,
                          ""name"": {
                            ""firstName"": ""Jon"",
                            ""middleName"": ""V"",
                            ""lastName"": ""Yang""
                          },
                          ""suffix"": null,
                          ""emailAddresses"": [
                            {
                              ""emailAddress"": ""jon24@adventure-works.com""
                            }
                          ],
                          ""phoneNumbers"": [
                            {
                              ""phoneNumberType"": ""Cell"",
                              ""phoneNumber"": ""398-555-0132""
                            }
                          ]
                        }
                      }
                    ],
                    ""accountNumber"": ""AW00000001"",
                    ""territory"": ""Northwest"",
                    ""addresses"": [
                      {
                        ""addressType"": ""Main Office"",
                        ""address"": {
                          ""addressLine1"": ""2251 Elliot Avenue"",
                          ""addressLine2"": null,
                          ""postalCode"": ""98104"",
                          ""city"": ""Seattle"",
                          ""stateProvinceCode"": ""WA"",
                          ""countryRegionCode"": ""US""
                        }
                      }
                    ]
                  }";

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            Core.Handlers.GetCustomer.StoreCustomer,
                            Core.Handlers.GetCustomer.IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var storeCustomer = JsonSerializer.Deserialize<Core.Handlers.GetCustomer.StoreCustomer>(json, serializeOptions);

                //Assert
                storeCustomer?.AccountNumber.Should().Be("AW00000001");
                storeCustomer?.CustomerType.Should().Be(AW.SharedKernel.Interfaces.CustomerType.Store);
                storeCustomer?.Name.Should().Be("A Bike Store");
                storeCustomer?.SalesPerson.Should().Be("Pamela O Ansman-Wolfe");
                storeCustomer?.Territory.Should().Be("Northwest");
                storeCustomer?.Contacts?.Count.Should().Be(1);
                storeCustomer?.Contacts?[0].ContactType.Should().Be("Owner");
                storeCustomer?.Contacts?[0].ContactPerson?.Title.Should().BeNull();
                storeCustomer?.Contacts?[0].ContactPerson?.Name?.FirstName.Should().Be("Jon");
                storeCustomer?.Contacts?[0].ContactPerson?.Name?.MiddleName.Should().Be("V");
                storeCustomer?.Contacts?[0].ContactPerson?.Name?.LastName.Should().Be("Yang");
                storeCustomer?.Contacts?[0].ContactPerson?.Suffix.Should().BeNull();
                storeCustomer?.Contacts?[0].ContactPerson?.EmailAddresses!.Count.Should().Be(1);
                storeCustomer?.Contacts?[0].ContactPerson?.EmailAddresses![0].EmailAddress.Should().Be("jon24@adventure-works.com");
                storeCustomer?.Contacts?[0].ContactPerson?.PhoneNumbers!.Count.Should().Be(1);
                storeCustomer?.Contacts?[0].ContactPerson?.PhoneNumbers![0].PhoneNumberType.Should().Be("Cell");
                storeCustomer?.Contacts?[0].ContactPerson?.PhoneNumbers![0].PhoneNumber.Should().Be("398-555-0132");
                storeCustomer?.Addresses.Count.Should().Be(1);
                storeCustomer?.Addresses[0].AddressType.Should().Be("Main Office");
                storeCustomer?.Addresses[0].Address?.AddressLine1.Should().Be("2251 Elliot Avenue");
                storeCustomer?.Addresses[0].Address?.AddressLine2.Should().BeNull();
                storeCustomer?.Addresses[0].Address?.PostalCode.Should().Be("98104");
                storeCustomer?.Addresses[0].Address?.City.Should().Be("Seattle");
                storeCustomer?.Addresses[0].Address?.StateProvinceCode.Should().Be("WA");
                storeCustomer?.Addresses[0].Address?.CountryRegionCode.Should().Be("US");
            }

            [Theory]
            [AutoMoqData]
            public void Deserialize_WithValidJson_ReturnsIndividualCustomer(
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, Core.Handlers.GetCustomer.StoreCustomer, Core.Handlers.GetCustomer.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var json = @"{
                  ""customerType"": ""Individual"",
                  ""person"": {
                    ""title"": null,
                    ""name"": { 
                        ""firstName"": ""Jon"",
                        ""middleName"": ""V"",
                        ""lastName"": ""Yang""
                    },
                    ""suffix"": null,
                    ""emailAddresses"": [
                      {
                        ""emailAddress"": ""jon24@adventure-works.com""
                      }
                    ],
                    ""phoneNumbers"": [
                      {
                        ""phoneNumberType"": ""Cell"",
                        ""phoneNumber"": ""398-555-0132""
                      }
                    ]
                  },
                  ""accountNumber"": ""AW00011000"",
                  ""territory"": ""Australia"",
                  ""addresses"": [
                    {
                      ""addressType"": ""Home"",
                      ""address"": {
                        ""addressLine1"": ""3761 N. 14th St"",
                        ""addressLine2"": null,
                        ""postalCode"": ""4700"",
                        ""city"": ""Rockhampton"",
                        ""stateProvinceCode"": ""QLD"",
                        ""countryRegionCode"": ""AU""
                      }
                    }
                  ]
                }";

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            Core.Handlers.GetCustomer.StoreCustomer,
                            Core.Handlers.GetCustomer.IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var individualCustomer = JsonSerializer.Deserialize<IndividualCustomer>(json, serializeOptions);

                //Assert
                individualCustomer?.AccountNumber.Should().Be("AW00011000");
                individualCustomer?.CustomerType.Should().Be(AW.SharedKernel.Interfaces.CustomerType.Individual);
                individualCustomer?.Territory.Should().Be("Australia");
                individualCustomer?.Person?.Title.Should().BeNull();
                individualCustomer?.Person?.Name?.FirstName.Should().Be("Jon");
                individualCustomer?.Person?.Name?.MiddleName.Should().Be("V");
                individualCustomer?.Person?.Name?.LastName.Should().Be("Yang");
                individualCustomer?.Person?.Suffix.Should().BeNull();
                individualCustomer?.Person?.EmailAddresses.Count.Should().Be(1);
                individualCustomer?.Person?.EmailAddresses[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                individualCustomer?.Person?.PhoneNumbers.Count.Should().Be(1);
                individualCustomer?.Person?.PhoneNumbers[0].PhoneNumberType.Should().Be("Cell");
                individualCustomer?.Person?.PhoneNumbers[0].PhoneNumber.Should().Be("398-555-0132");
                individualCustomer?.Addresses.Count.Should().Be(1);
                individualCustomer?.Addresses?[0].AddressType.Should().Be("Home");
                individualCustomer?.Addresses?[0].Address?.AddressLine1.Should().Be("3761 N. 14th St");
                individualCustomer?.Addresses?[0].Address?.AddressLine2.Should().BeNull();
                individualCustomer?.Addresses?[0].Address?.PostalCode.Should().Be("4700");
                individualCustomer?.Addresses?[0].Address?.City.Should().Be("Rockhampton");
                individualCustomer?.Addresses?[0].Address?.StateProvinceCode.Should().Be("QLD");
                individualCustomer?.Addresses?[0].Address?.CountryRegionCode.Should().Be("AU");
            }
        }
        
        public class GetCustomer
        {
            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomerStore_ReturnsStoreCustomer(
                StoreCustomer storeCustomer,
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, StoreCustomer, IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            StoreCustomer,
                            IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var jsonString = JsonSerializer.Serialize(storeCustomer, serializeOptions);
                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;
                var contacts = root.GetProperty("contacts").EnumerateArray().ToList();
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();

                //Assert
                root.GetProperty("customerType").GetString().Should().Be(storeCustomer.CustomerType.ToString());
                root.GetProperty("name").GetString().Should().Be(storeCustomer.Name);
                root.GetProperty("salesPerson").GetString().Should().Be(storeCustomer.SalesPerson);
                root.GetProperty("accountNumber").GetString().Should().Be(storeCustomer.AccountNumber);
                root.GetProperty("territory").GetString().Should().Be(storeCustomer.Territory);

                for (int i = 0; i < contacts.Count; i++)
                {
                    var contact = contacts[i];
                    var contactPerson = contacts[i].GetProperty("contactPerson");
                    contact.GetProperty("contactType").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactType);
                    contactPerson.GetProperty("title").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Title);
                    contactPerson.GetProperty("name").GetProperty("firstName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.FirstName);
                    contactPerson.GetProperty("name").GetProperty("middleName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.MiddleName);
                    contactPerson.GetProperty("name").GetProperty("lastName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Name!.LastName);
                    contactPerson.GetProperty("suffix").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson!.Suffix);

                    var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();

                    for (int j = 0; j < emailAddresses.Count; j++)
                    {
                        var emailAddress = emailAddresses[j];
                        emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.EmailAddresses![j].EmailAddress);
                    }

                    var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();

                    for (int j = 0; j < phoneNumbers.Count; j++)
                    {
                        var phoneNumber = phoneNumbers[j];
                        phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.PhoneNumbers![j].PhoneNumberType);
                        phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson!.PhoneNumbers![j].PhoneNumber);
                    }
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        storeCustomer.Addresses![i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address!.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomerIndividual_ReturnsIndividualCustomer(
                IndividualCustomer individualCustomer,
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, StoreCustomer, Core.Handlers.GetCustomer.IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            StoreCustomer,
                            IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var jsonString = JsonSerializer.Serialize(individualCustomer, serializeOptions);
                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;
                var person = root.GetProperty("person");
                var emailAddresses = person.GetProperty("emailAddresses").EnumerateArray().ToList();
                var phoneNumbers = person.GetProperty("phoneNumbers").EnumerateArray().ToList();
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Individual");
                root.GetProperty("accountNumber").GetString().Should().Be(individualCustomer.AccountNumber);
                root.GetProperty("territory").GetString().Should().Be(individualCustomer.Territory);

                person.GetProperty("title").GetString().Should().Be(individualCustomer.Person!.Title);
                person.GetProperty("name").GetProperty("firstName").GetString().Should().Be(individualCustomer.Person.Name!.FirstName);
                person.GetProperty("name").GetProperty("middleName").GetString().Should().Be(individualCustomer.Person.Name.MiddleName);
                person.GetProperty("name").GetProperty("lastName").GetString().Should().Be(individualCustomer.Person.Name.LastName);
                person.GetProperty("suffix").GetString().Should().Be(individualCustomer.Person.Suffix);

                for (int i = 0; i < emailAddresses.Count; i++)
                {
                    var emailAddress = emailAddresses[i];
                    emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                        individualCustomer.Person.EmailAddresses![i].EmailAddress);
                }

                for (int i = 0; i < phoneNumbers.Count; i++)
                {
                    var phoneNumber = phoneNumbers[i];
                    phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers![i].PhoneNumberType);
                    phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumber);
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        individualCustomer.Addresses![i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address!.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Deserialize_WithValidJson_ReturnsStoreCustomer(
                Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, StoreCustomer, IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var json = @"{
                    ""customerType"": ""Store"",
                    ""name"": ""A Bike Store"",
                    ""salesPerson"": ""Pamela O Ansman-Wolfe"",
                    ""contacts"": [
                      {
                        ""contactType"": ""Owner"",
                        ""contactPerson"": {
                          ""title"": null,
                          ""name"": { 
                            ""firstName"": ""Jon"",
                            ""middleName"": ""V"",
                            ""lastName"": ""Yang""
                          },
                          ""suffix"": null,
                          ""emailAddresses"": [
                            {
                              ""emailAddress"": ""jon24@adventure-works.com""
                            }
                          ],
                          ""phoneNumbers"": [
                            {
                              ""phoneNumberType"": ""Cell"",
                              ""phoneNumber"": ""398-555-0132""
                            }
                          ]
                        }
                      }
                    ],
                    ""accountNumber"": ""AW00000001"",
                    ""territory"": ""Northwest"",
                    ""addresses"": [
                      {
                        ""addressType"": ""Main Office"",
                        ""address"": {
                          ""addressLine1"": ""2251 Elliot Avenue"",
                          ""addressLine2"": null,
                          ""postalCode"": ""98104"",
                          ""city"": ""Seattle"",
                          ""stateProvinceCode"": ""WA"",
                          ""countryRegionCode"": ""US""
                        }
                      }
                    ]
                  }";

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            StoreCustomer,
                            IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var storeCustomer = JsonSerializer.Deserialize<StoreCustomer>(json, serializeOptions);

                //Assert
                storeCustomer?.AccountNumber.Should().Be("AW00000001");
                storeCustomer?.CustomerType.Should().Be(AW.SharedKernel.Interfaces.CustomerType.Store);
                storeCustomer?.Name.Should().Be("A Bike Store");
                storeCustomer?.SalesPerson.Should().Be("Pamela O Ansman-Wolfe");
                storeCustomer?.Territory.Should().Be("Northwest");
                storeCustomer?.Contacts.Count.Should().Be(1);
                storeCustomer?.Contacts[0].ContactType.Should().Be("Owner");
                storeCustomer?.Contacts[0].ContactPerson?.Title.Should().BeNull();
                storeCustomer?.Contacts[0].ContactPerson?.Name?.FirstName.Should().Be("Jon");
                storeCustomer?.Contacts[0].ContactPerson?.Name?.MiddleName.Should().Be("V");
                storeCustomer?.Contacts[0].ContactPerson?.Name?.LastName.Should().Be("Yang");
                storeCustomer?.Contacts[0].ContactPerson?.Suffix.Should().BeNull();
                storeCustomer?.Contacts[0].ContactPerson?.EmailAddresses?.Count.Should().Be(1);
                storeCustomer?.Contacts[0].ContactPerson?.EmailAddresses?[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                storeCustomer?.Contacts[0].ContactPerson?.PhoneNumbers?.Count.Should().Be(1);
                storeCustomer?.Contacts[0].ContactPerson?.PhoneNumbers?[0].PhoneNumberType.Should().Be("Cell");
                storeCustomer?.Contacts[0].ContactPerson?.PhoneNumbers?[0].PhoneNumber.Should().Be("398-555-0132");
                storeCustomer?.Addresses!.Count.Should().Be(1);
                storeCustomer?.Addresses![0].AddressType.Should().Be("Main Office");
                storeCustomer?.Addresses![0].Address!.AddressLine1.Should().Be("2251 Elliot Avenue");
                storeCustomer?.Addresses![0].Address!.AddressLine2.Should().BeNull();
                storeCustomer?.Addresses![0].Address!.PostalCode.Should().Be("98104");
                storeCustomer?.Addresses![0].Address!.City.Should().Be("Seattle");
                storeCustomer?.Addresses![0].Address!.StateProvinceCode.Should().Be("WA");
                storeCustomer?.Addresses![0].Address!.CountryRegionCode.Should().Be("US");
            }

            [Theory]
            [AutoMoqData]
            public void Deserialize_WithValidJson_ReturnsIndividualCustomer(
                   Mock<ILogger<CustomerConverter<Core.Handlers.GetCustomer.Customer, StoreCustomer, IndividualCustomer>>> mockLogger
            )
            {
                //Arrange
                var json = @"{
                  ""customerType"": ""Individual"",
                  ""person"": {
                    ""title"": null,
                    ""name"": {
                        ""firstName"": ""Jon"",
                        ""middleName"": ""V"",
                        ""lastName"": ""Yang""
                    },
                    ""suffix"": null,
                    ""emailAddresses"": [
                      {
                        ""emailAddress"": ""jon24@adventure-works.com""
                      }
                    ],
                    ""phoneNumbers"": [
                      {
                        ""phoneNumberType"": ""Cell"",
                        ""phoneNumber"": ""398-555-0132""
                      }
                    ]
                  },
                  ""accountNumber"": ""AW00011000"",
                  ""territory"": ""Australia"",
                  ""addresses"": [
                    {
                      ""addressType"": ""Home"",
                      ""address"": {
                        ""addressLine1"": ""3761 N. 14th St"",
                        ""addressLine2"": null,
                        ""postalCode"": ""4700"",
                        ""city"": ""Rockhampton"",
                        ""stateProvinceCode"": ""QLD"",
                        ""countryRegionCode"": ""AU""
                      }
                    }
                  ]
                }";

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Core.Handlers.GetCustomer.Customer,
                            StoreCustomer,
                            IndividualCustomer>(
                                mockLogger.Object
                            )
                    }
                };

                //Act
                var individualCustomer = JsonSerializer.Deserialize<IndividualCustomer>(json, serializeOptions);

                //Assert
                individualCustomer?.AccountNumber.Should().Be("AW00011000");
                individualCustomer?.CustomerType.Should().Be(AW.SharedKernel.Interfaces.CustomerType.Individual);
                individualCustomer?.Territory.Should().Be("Australia");
                individualCustomer?.Person?.Title.Should().BeNull();
                individualCustomer?.Person?.Name!.FirstName.Should().Be("Jon");
                individualCustomer?.Person?.Name!.MiddleName.Should().Be("V");
                individualCustomer?.Person?.Name!.LastName.Should().Be("Yang");
                individualCustomer?.Person?.Suffix.Should().BeNull();
                individualCustomer?.Person?.EmailAddresses!.Count.Should().Be(1);
                individualCustomer?.Person?.EmailAddresses![0].EmailAddress.Should().Be("jon24@adventure-works.com");
                individualCustomer?.Person?.PhoneNumbers!.Count.Should().Be(1);
                individualCustomer?.Person?.PhoneNumbers![0].PhoneNumberType.Should().Be("Cell");
                individualCustomer?.Person?.PhoneNumbers![0].PhoneNumber.Should().Be("398-555-0132");
                individualCustomer?.Addresses!.Count.Should().Be(1);
                individualCustomer?.Addresses![0].AddressType.Should().Be("Home");
                individualCustomer?.Addresses![0].Address!.AddressLine1.Should().Be("3761 N. 14th St");
                individualCustomer?.Addresses![0].Address!.AddressLine2.Should().BeNull();
                individualCustomer?.Addresses![0].Address!.PostalCode.Should().Be("4700");
                individualCustomer?.Addresses![0].Address!.City.Should().Be("Rockhampton");
                individualCustomer?.Addresses![0].Address!.StateProvinceCode.Should().Be("QLD");
                individualCustomer?.Addresses![0].Address!.CountryRegionCode.Should().Be("AU");
            }
        }
    }
}
