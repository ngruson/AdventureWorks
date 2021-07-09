using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.SharedKernel.JsonConverters;
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
            [Fact]
            public void Serialize_WithGetCustomersStore_ReturnsStoreCustomer()
            {
                //Arrange
                var storeCustomer = new TestBuilders.GetCustomers.StoreCustomerBuilder()
                    .WithTestValues()
                    .Build();

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
                    }
                };

                //Act
                var jsonString = JsonSerializer.Serialize(storeCustomer, serializeOptions);
                var jsonDocument = JsonDocument.Parse(jsonString);
                var root = jsonDocument.RootElement;
                var contacts = root.GetProperty("contacts").EnumerateArray().ToList();
                var contactPerson = contacts[0].GetProperty("contactPerson");
                var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();
                var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();
                var address = addresses[0].GetProperty("address");

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Store");
                root.GetProperty("name").GetString().Should().Be("A Bike Store");
                root.GetProperty("salesPerson").GetString().Should().Be("Pamela O Ansman-Wolfe");
                root.GetProperty("accountNumber").GetString().Should().Be("AW00000001");
                root.GetProperty("territory").GetString().Should().Be("Northwest");

                contacts[0].GetProperty("contactType").GetString().Should().Be("Owner");
                contactPerson.GetProperty("title").GetString().Should().BeNull();
                contactPerson.GetProperty("firstName").GetString().Should().Be("Jon");
                contactPerson.GetProperty("middleName").GetString().Should().Be("V");
                contactPerson.GetProperty("lastName").GetString().Should().Be("Yang");
                contactPerson.GetProperty("suffix").GetString().Should().BeNull();
                emailAddresses[0].GetProperty("emailAddress").GetString().Should().Be("jon24@adventure-works.com");
                phoneNumbers[0].GetProperty("phoneNumberType").GetString().Should().Be("Cell");
                phoneNumbers[0].GetProperty("phoneNumber").GetString().Should().Be("398-555-0132");

                addresses[0].GetProperty("addressType").GetString().Should().Be("Main Office");
                address.GetProperty("addressLine1").GetString().Should().Be("2251 Elliot Avenue");
                address.GetProperty("addressLine2").GetString().Should().BeNull();
                address.GetProperty("postalCode").GetString().Should().Be("98104");
                address.GetProperty("city").GetString().Should().Be("Seattle");
                address.GetProperty("stateProvinceCode").GetString().Should().Be("WA");
                address.GetProperty("countryRegionCode").GetString().Should().Be("US");
            }

            [Fact]
            public void Serialize_WithGetCustomersIndividual_ReturnsIndividualCustomer()
            {
                //Arrange
                var individualCustomer = new TestBuilders.GetCustomers.IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build();

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true,
                    Converters =
                    {
                        new JsonStringEnumConverter(),
                        new CustomerConverter<
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
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
                var address = addresses[0].GetProperty("address");

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Individual");
                root.GetProperty("accountNumber").GetString().Should().Be("AW00011000");
                root.GetProperty("territory").GetString().Should().Be("Australia");

                person.GetProperty("title").GetString().Should().BeNull();
                person.GetProperty("firstName").GetString().Should().Be("Jon");
                person.GetProperty("middleName").GetString().Should().Be("V");
                person.GetProperty("lastName").GetString().Should().Be("Yang");
                person.GetProperty("suffix").GetString().Should().BeNull();
                emailAddresses[0].GetProperty("emailAddress").GetString().Should().Be("jon24@adventure-works.com");
                phoneNumbers[0].GetProperty("phoneNumberType").GetString().Should().Be("Cell");
                phoneNumbers[0].GetProperty("phoneNumber").GetString().Should().Be("398-555-0132");

                addresses[0].GetProperty("addressType").GetString().Should().Be("Home");
                address.GetProperty("addressLine1").GetString().Should().Be("3761 N. 14th St");
                address.GetProperty("addressLine2").GetString().Should().BeNull();
                address.GetProperty("postalCode").GetString().Should().Be("4700");
                address.GetProperty("city").GetString().Should().Be("Rockhampton");
                address.GetProperty("stateProvinceCode").GetString().Should().Be("QLD");
                address.GetProperty("countryRegionCode").GetString().Should().Be("AU");
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
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
                    }
                };

                //Act
                var storeCustomer = JsonSerializer.Deserialize<Models.GetCustomers.StoreCustomer>(json, serializeOptions);

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
                            Models.GetCustomers.Customer,
                            Models.GetCustomers.StoreCustomer,
                            Models.GetCustomers.IndividualCustomer>()
                    }
                };

                //Act
                var individualCustomer = JsonSerializer.Deserialize<Models.GetCustomers.IndividualCustomer>(json, serializeOptions);

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
            [Fact]
            public void Serialize_WithGetCustomerStore_ReturnsStoreCustomer()
            {
                //Arrange
                var storeCustomer = new TestBuilders.GetCustomer.StoreCustomerBuilder()
                    .WithTestValues()
                    .Build();

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
                var contactPerson = contacts[0].GetProperty("contactPerson");
                var emailAddresses = contactPerson.GetProperty("emailAddresses").EnumerateArray().ToList();
                var phoneNumbers = contactPerson.GetProperty("phoneNumbers").EnumerateArray().ToList();
                var addresses = root.GetProperty("addresses").EnumerateArray().ToList();
                var address = addresses[0].GetProperty("address");

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Store");
                root.GetProperty("name").GetString().Should().Be("A Bike Store");
                root.GetProperty("salesPerson").GetString().Should().Be("Pamela O Ansman-Wolfe");
                root.GetProperty("accountNumber").GetString().Should().Be("AW00000001");
                root.GetProperty("territory").GetString().Should().Be("Northwest");

                contacts[0].GetProperty("contactType").GetString().Should().Be("Owner");
                contactPerson.GetProperty("title").GetString().Should().BeNull();
                contactPerson.GetProperty("firstName").GetString().Should().Be("Jon");
                contactPerson.GetProperty("middleName").GetString().Should().Be("V");
                contactPerson.GetProperty("lastName").GetString().Should().Be("Yang");
                contactPerson.GetProperty("suffix").GetString().Should().BeNull();
                emailAddresses[0].GetProperty("emailAddress").GetString().Should().Be("jon24@adventure-works.com");
                phoneNumbers[0].GetProperty("phoneNumberType").GetString().Should().Be("Cell");
                phoneNumbers[0].GetProperty("phoneNumber").GetString().Should().Be("398-555-0132");

                addresses[0].GetProperty("addressType").GetString().Should().Be("Main Office");
                address.GetProperty("addressLine1").GetString().Should().Be("2251 Elliot Avenue");
                address.GetProperty("addressLine2").GetString().Should().BeNull();
                address.GetProperty("postalCode").GetString().Should().Be("98104");
                address.GetProperty("city").GetString().Should().Be("Seattle");
                address.GetProperty("stateProvinceCode").GetString().Should().Be("WA");
                address.GetProperty("countryRegionCode").GetString().Should().Be("US");
            }

            [Fact]
            public void Serialize_WithGetCustomerIndividual_ReturnsIndividualCustomer()
            {
                //Arrange
                var individualCustomer = new TestBuilders.GetCustomer.IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build();

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
                var address = addresses[0].GetProperty("address");

                //Assert
                root.GetProperty("customerType").GetString().Should().Be("Individual");
                root.GetProperty("accountNumber").GetString().Should().Be("AW00011000");
                root.GetProperty("territory").GetString().Should().Be("Australia");

                person.GetProperty("title").GetString().Should().BeNull();
                person.GetProperty("firstName").GetString().Should().Be("Jon");
                person.GetProperty("middleName").GetString().Should().Be("V");
                person.GetProperty("lastName").GetString().Should().Be("Yang");
                person.GetProperty("suffix").GetString().Should().BeNull();
                emailAddresses[0].GetProperty("emailAddress").GetString().Should().Be("jon24@adventure-works.com");
                phoneNumbers[0].GetProperty("phoneNumberType").GetString().Should().Be("Cell");
                phoneNumbers[0].GetProperty("phoneNumber").GetString().Should().Be("398-555-0132");

                addresses[0].GetProperty("addressType").GetString().Should().Be("Home");
                address.GetProperty("addressLine1").GetString().Should().Be("3761 N. 14th St");
                address.GetProperty("addressLine2").GetString().Should().BeNull();
                address.GetProperty("postalCode").GetString().Should().Be("4700");
                address.GetProperty("city").GetString().Should().Be("Rockhampton");
                address.GetProperty("stateProvinceCode").GetString().Should().Be("QLD");
                address.GetProperty("countryRegionCode").GetString().Should().Be("AU");
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