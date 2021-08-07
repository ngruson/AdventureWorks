using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.SharedKernel.JsonConverters;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace AW.Services.Customer.REST.API.UnitTests
{
    public class CustomerConverterUnitTests
    {
        public class GetCustomers
        {
            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomersStore_ReturnsStoreCustomer(
                Core.Handlers.GetCustomer.StoreCustomerDto storeCustomer
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
                            Core.Models.GetCustomers.Customer,
                            Core.Models.GetCustomers.StoreCustomer,
                            Core.Models.GetCustomers.IndividualCustomer>()
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
                        storeCustomer.Contacts[i].ContactPerson.Title);
                    contactPerson.GetProperty("firstName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.FirstName);
                    contactPerson.GetProperty("middleName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.MiddleName);
                    contactPerson.GetProperty("lastName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.LastName);
                    contactPerson.GetProperty("suffix").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.Suffix);

                    var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();

                    for (int j = 0; j < emailAddresses.Count; j++)
                    {
                        var emailAddress = emailAddresses[j];
                        emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.EmailAddresses[j].EmailAddress);
                    }

                    var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();

                    for (int j = 0; j < phoneNumbers.Count; j++)
                    {
                        var phoneNumber = phoneNumbers[j];
                        phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.PhoneNumbers[j].PhoneNumberType);
                        phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.PhoneNumbers[j].PhoneNumber);
                    }
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        storeCustomer.Addresses[i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomersIndividual_ReturnsIndividualCustomer(
                Core.Handlers.GetCustomer.IndividualCustomerDto individualCustomer
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
                            Core.Models.GetCustomers.Customer,
                            Core.Models.GetCustomers.StoreCustomer,
                            Core.Models.GetCustomers.IndividualCustomer>()
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

                person.GetProperty("title").GetString().Should().Be(individualCustomer.Person.Title);
                person.GetProperty("firstName").GetString().Should().Be(individualCustomer.Person.FirstName);
                person.GetProperty("middleName").GetString().Should().Be(individualCustomer.Person.MiddleName);
                person.GetProperty("lastName").GetString().Should().Be(individualCustomer.Person.LastName);
                person.GetProperty("suffix").GetString().Should().Be(individualCustomer.Person.Suffix);

                for (int i = 0; i < emailAddresses.Count; i++)
                {
                    var emailAddress = emailAddresses[i];
                    emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                        individualCustomer.Person.EmailAddresses[i].EmailAddress);
                }

                for (int i = 0; i < phoneNumbers.Count; i++)
                {
                    var phoneNumber = phoneNumbers[i];
                    phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumberType);
                    phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumber);
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        individualCustomer.Addresses[i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.CountryRegionCode);
                }
            }

            [Fact]
            public void Deserialize_WithValidJson_ReturnsStoreCustomer()
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
                          ""firstName"": ""Jon"",
                          ""middleName"": ""V"",
                          ""lastName"": ""Yang"",
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
                            Core.Models.GetCustomers.Customer,
                            Core.Models.GetCustomers.StoreCustomer,
                            Core.Models.GetCustomers.IndividualCustomer>()
                    }
                };

                //Act
                var storeCustomer = JsonSerializer.Deserialize<Core.Models.GetCustomers.StoreCustomer>(json, serializeOptions);

                //Assert
                storeCustomer.AccountNumber.Should().Be("AW00000001");
                storeCustomer.CustomerType.Should().Be(CustomerType.Store);
                storeCustomer.Name.Should().Be("A Bike Store");
                storeCustomer.SalesPerson.Should().Be("Pamela O Ansman-Wolfe");
                storeCustomer.Territory.Should().Be("Northwest");
                storeCustomer.Contacts.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactType.Should().Be("Owner");
                storeCustomer.Contacts[0].ContactPerson.Title.Should().BeNull();
                storeCustomer.Contacts[0].ContactPerson.FirstName.Should().Be("Jon");
                storeCustomer.Contacts[0].ContactPerson.MiddleName.Should().Be("V");
                storeCustomer.Contacts[0].ContactPerson.LastName.Should().Be("Yang");
                storeCustomer.Contacts[0].ContactPerson.Suffix.Should().BeNull();
                storeCustomer.Contacts[0].ContactPerson.EmailAddresses.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactPerson.EmailAddresses[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers[0].PhoneNumberType.Should().Be("Cell");
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers[0].PhoneNumber.Should().Be("398-555-0132");
                storeCustomer.Addresses.Count.Should().Be(1);
                storeCustomer.Addresses[0].AddressType.Should().Be("Main Office");
                storeCustomer.Addresses[0].Address.AddressLine1.Should().Be("2251 Elliot Avenue");
                storeCustomer.Addresses[0].Address.AddressLine2.Should().BeNull();
                storeCustomer.Addresses[0].Address.PostalCode.Should().Be("98104");
                storeCustomer.Addresses[0].Address.City.Should().Be("Seattle");
                storeCustomer.Addresses[0].Address.StateProvinceCode.Should().Be("WA");
                storeCustomer.Addresses[0].Address.CountryRegionCode.Should().Be("US");
            }

            [Fact]
            public void Deserialize_WithValidJson_ReturnsIndividualCustomer()
            {
                //Arrange
                var json = @"{
                  ""customerType"": ""Individual"",
                  ""person"": {
                    ""title"": null,
                    ""firstName"": ""Jon"",
                    ""middleName"": ""V"",
                    ""lastName"": ""Yang"",
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
                            Core.Models.GetCustomers.Customer,
                            Core.Models.GetCustomers.StoreCustomer,
                            Core.Models.GetCustomers.IndividualCustomer>()
                    }
                };

                //Act
                var individualCustomer = JsonSerializer.Deserialize<Core.Models.GetCustomers.IndividualCustomer>(json, serializeOptions);

                //Assert
                individualCustomer.AccountNumber.Should().Be("AW00011000");
                individualCustomer.CustomerType.Should().Be(CustomerType.Individual);
                individualCustomer.Territory.Should().Be("Australia");
                individualCustomer.Person.Title.Should().BeNull();
                individualCustomer.Person.FirstName.Should().Be("Jon");
                individualCustomer.Person.MiddleName.Should().Be("V");
                individualCustomer.Person.LastName.Should().Be("Yang");
                individualCustomer.Person.Suffix.Should().BeNull();
                individualCustomer.Person.EmailAddresses.Count.Should().Be(1);
                individualCustomer.Person.EmailAddresses[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                individualCustomer.Person.PhoneNumbers.Count.Should().Be(1);
                individualCustomer.Person.PhoneNumbers[0].PhoneNumberType.Should().Be("Cell");
                individualCustomer.Person.PhoneNumbers[0].PhoneNumber.Should().Be("398-555-0132");
                individualCustomer.Addresses.Count.Should().Be(1);
                individualCustomer.Addresses[0].AddressType.Should().Be("Home");
                individualCustomer.Addresses[0].Address.AddressLine1.Should().Be("3761 N. 14th St");
                individualCustomer.Addresses[0].Address.AddressLine2.Should().BeNull();
                individualCustomer.Addresses[0].Address.PostalCode.Should().Be("4700");
                individualCustomer.Addresses[0].Address.City.Should().Be("Rockhampton");
                individualCustomer.Addresses[0].Address.StateProvinceCode.Should().Be("QLD");
                individualCustomer.Addresses[0].Address.CountryRegionCode.Should().Be("AU");
            }
        }
        
        public class GetCustomer
        {
            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomerStore_ReturnsStoreCustomer(
                Core.Handlers.GetCustomer.StoreCustomerDto storeCustomer
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
                        Models.GetCustomer.Customer,
                        Models.GetCustomer.StoreCustomer,
                        Models.GetCustomer.IndividualCustomer>()
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
                        storeCustomer.Contacts[i].ContactPerson.Title);
                    contactPerson.GetProperty("firstName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.FirstName);
                    contactPerson.GetProperty("middleName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.MiddleName);
                    contactPerson.GetProperty("lastName").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.LastName);
                    contactPerson.GetProperty("suffix").GetString().Should().Be(
                        storeCustomer.Contacts[i].ContactPerson.Suffix);

                    var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();

                    for (int j = 0; j < emailAddresses.Count; j++)
                    {
                        var emailAddress = emailAddresses[j];
                        emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.EmailAddresses[j].EmailAddress);
                    }

                    var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();

                    for (int j = 0; j < phoneNumbers.Count; j++)
                    {
                        var phoneNumber = phoneNumbers[j];
                        phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.PhoneNumbers[j].PhoneNumberType);
                        phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                            storeCustomer.Contacts[i].ContactPerson.PhoneNumbers[j].PhoneNumber);
                    }
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        storeCustomer.Addresses[i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        storeCustomer.Addresses[i].Address.CountryRegionCode);
                }
            }

            [Theory]
            [AutoMoqData]
            public void Serialize_WithGetCustomerIndividual_ReturnsIndividualCustomer(
                Core.Handlers.GetCustomer.IndividualCustomerDto individualCustomer
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
                            Models.GetCustomer.Customer,
                            Models.GetCustomer.StoreCustomer,
                            Models.GetCustomer.IndividualCustomer>()
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

                person.GetProperty("title").GetString().Should().Be(individualCustomer.Person.Title);
                person.GetProperty("firstName").GetString().Should().Be(individualCustomer.Person.FirstName);
                person.GetProperty("middleName").GetString().Should().Be(individualCustomer.Person.MiddleName);
                person.GetProperty("lastName").GetString().Should().Be(individualCustomer.Person.LastName);
                person.GetProperty("suffix").GetString().Should().Be(individualCustomer.Person.Suffix);

                for (int i = 0; i < emailAddresses.Count; i++)
                {
                    var emailAddress = emailAddresses[i];
                    emailAddress.GetProperty("emailAddress").GetString().Should().Be(
                        individualCustomer.Person.EmailAddresses[i].EmailAddress);
                }

                for (int i = 0; i < phoneNumbers.Count; i++)
                {
                    var phoneNumber = phoneNumbers[i];
                    phoneNumber.GetProperty("phoneNumberType").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumberType);
                    phoneNumber.GetProperty("phoneNumber").GetString().Should().Be(
                        individualCustomer.Person.PhoneNumbers[i].PhoneNumber);
                }

                for (int i = 0; i < addresses.Count; i++)
                {
                    var addressItem = addresses[i];
                    var address = addressItem.GetProperty("address");
                    addressItem.GetProperty("addressType").GetString().Should().Be(
                        individualCustomer.Addresses[i].AddressType);
                    address.GetProperty("addressLine1").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.AddressLine1);
                    address.GetProperty("addressLine2").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.AddressLine2);
                    address.GetProperty("postalCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.PostalCode);
                    address.GetProperty("city").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.City);
                    address.GetProperty("stateProvinceCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.StateProvinceCode);
                    address.GetProperty("countryRegionCode").GetString().Should().Be(
                        individualCustomer.Addresses[i].Address.CountryRegionCode);
                }
            }

            [Fact]
            public void Deserialize_WithValidJson_ReturnsStoreCustomer()
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
                          ""firstName"": ""Jon"",
                          ""middleName"": ""V"",
                          ""lastName"": ""Yang"",
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
                        Models.GetCustomer.Customer,
                        Models.GetCustomer.StoreCustomer,
                        Models.GetCustomer.IndividualCustomer>()
                }
                };

                //Act
                var storeCustomer = JsonSerializer.Deserialize<Models.GetCustomer.StoreCustomer>(json, serializeOptions);

                //Assert
                storeCustomer.AccountNumber.Should().Be("AW00000001");
                storeCustomer.CustomerType.Should().Be(CustomerType.Store);
                storeCustomer.Name.Should().Be("A Bike Store");
                storeCustomer.SalesPerson.Should().Be("Pamela O Ansman-Wolfe");
                storeCustomer.Territory.Should().Be("Northwest");
                storeCustomer.Contacts.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactType.Should().Be("Owner");
                storeCustomer.Contacts[0].ContactPerson.Title.Should().BeNull();
                storeCustomer.Contacts[0].ContactPerson.FirstName.Should().Be("Jon");
                storeCustomer.Contacts[0].ContactPerson.MiddleName.Should().Be("V");
                storeCustomer.Contacts[0].ContactPerson.LastName.Should().Be("Yang");
                storeCustomer.Contacts[0].ContactPerson.Suffix.Should().BeNull();
                storeCustomer.Contacts[0].ContactPerson.EmailAddresses.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactPerson.EmailAddresses[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers.Count.Should().Be(1);
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers[0].PhoneNumberType.Should().Be("Cell");
                storeCustomer.Contacts[0].ContactPerson.PhoneNumbers[0].PhoneNumber.Should().Be("398-555-0132");
                storeCustomer.Addresses.Count.Should().Be(1);
                storeCustomer.Addresses[0].AddressType.Should().Be("Main Office");
                storeCustomer.Addresses[0].Address.AddressLine1.Should().Be("2251 Elliot Avenue");
                storeCustomer.Addresses[0].Address.AddressLine2.Should().BeNull();
                storeCustomer.Addresses[0].Address.PostalCode.Should().Be("98104");
                storeCustomer.Addresses[0].Address.City.Should().Be("Seattle");
                storeCustomer.Addresses[0].Address.StateProvinceCode.Should().Be("WA");
                storeCustomer.Addresses[0].Address.CountryRegionCode.Should().Be("US");
            }

            [Fact]
            public void Deserialize_WithValidJson_ReturnsIndividualCustomer()
            {
                //Arrange
                var json = @"{
                  ""customerType"": ""Individual"",
                  ""person"": {
                    ""title"": null,
                    ""firstName"": ""Jon"",
                    ""middleName"": ""V"",
                    ""lastName"": ""Yang"",
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
                            Models.GetCustomer.Customer,
                            Models.GetCustomer.StoreCustomer,
                            Models.GetCustomer.IndividualCustomer>()
                    }
                };

                //Act
                var individualCustomer = JsonSerializer.Deserialize<Models.GetCustomer.IndividualCustomer>(json, serializeOptions);

                //Assert
                individualCustomer.AccountNumber.Should().Be("AW00011000");
                individualCustomer.CustomerType.Should().Be(CustomerType.Individual);
                individualCustomer.Territory.Should().Be("Australia");
                individualCustomer.Person.Title.Should().BeNull();
                individualCustomer.Person.FirstName.Should().Be("Jon");
                individualCustomer.Person.MiddleName.Should().Be("V");
                individualCustomer.Person.LastName.Should().Be("Yang");
                individualCustomer.Person.Suffix.Should().BeNull();
                individualCustomer.Person.EmailAddresses.Count.Should().Be(1);
                individualCustomer.Person.EmailAddresses[0].EmailAddress.Should().Be("jon24@adventure-works.com");
                individualCustomer.Person.PhoneNumbers.Count.Should().Be(1);
                individualCustomer.Person.PhoneNumbers[0].PhoneNumberType.Should().Be("Cell");
                individualCustomer.Person.PhoneNumbers[0].PhoneNumber.Should().Be("398-555-0132");
                individualCustomer.Addresses.Count.Should().Be(1);
                individualCustomer.Addresses[0].AddressType.Should().Be("Home");
                individualCustomer.Addresses[0].Address.AddressLine1.Should().Be("3761 N. 14th St");
                individualCustomer.Addresses[0].Address.AddressLine2.Should().BeNull();
                individualCustomer.Addresses[0].Address.PostalCode.Should().Be("4700");
                individualCustomer.Addresses[0].Address.City.Should().Be("Rockhampton");
                individualCustomer.Addresses[0].Address.StateProvinceCode.Should().Be("QLD");
                individualCustomer.Addresses[0].Address.CountryRegionCode.Should().Be("AU");
            }
        }
    }
}