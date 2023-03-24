using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;
using FluentAssertions;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder
{
    public class IndividualCustomerViewModelBinderUnitTests
    {
        [Theory, AutoMoqData]
        public async Task BindModelGivenAddedEmailAddresses(
            IndividualCustomerViewModelBinder sut,
            IndividualCustomerViewModel viewModel
        )
        {
            //Arrange
            var formData = new FormCollection(
                new Dictionary<string, StringValues>
                {
                    { "AccountNumber", viewModel.AccountNumber },
                    { "CustomerName", viewModel.CustomerName },
                    { "Territory", viewModel.Territory },
                    { "Addresses[0].AddressType", viewModel.Addresses[0].AddressType },
                    { "Addresses[0].Address.AddressLine1", viewModel.Addresses[0].Address.AddressLine1 },
                    { "Addresses[0].Address.AddressLine2", viewModel.Addresses[0].Address.AddressLine2 },
                    { "Addresses[0].Address.PostalCode", viewModel.Addresses[0].Address.PostalCode },
                    { "Addresses[0].Address.City", viewModel.Addresses[0].Address.City },
                    { "Addresses[0].Address.StateProvinceCode", viewModel.Addresses[0].Address.StateProvinceCode },
                    { "Addresses[0].Address.CountryRegionCode", viewModel.Addresses[0].Address.CountryRegionCode },
                    { "Addresses[1].AddressType", viewModel.Addresses[1].AddressType },
                    { "Addresses[1].Address.AddressLine1", viewModel.Addresses[1].Address.AddressLine1 },
                    { "Addresses[1].Address.AddressLine2", viewModel.Addresses[1].Address.AddressLine2 },
                    { "Addresses[1].Address.PostalCode", viewModel.Addresses[1].Address.PostalCode },
                    { "Addresses[1].Address.City", viewModel.Addresses[1].Address.City },
                    { "Addresses[1].Address.StateProvinceCode", viewModel.Addresses[1].Address.StateProvinceCode },
                    { "Addresses[1].Address.CountryRegionCode", viewModel.Addresses[1].Address.CountryRegionCode },
                    { "Addresses[2].AddressType", viewModel.Addresses[2].AddressType },
                    { "Addresses[2].Address.AddressLine1", viewModel.Addresses[2].Address.AddressLine1 },
                    { "Addresses[2].Address.AddressLine2", viewModel.Addresses[2].Address.AddressLine2 },
                    { "Addresses[2].Address.PostalCode", viewModel.Addresses[2].Address.PostalCode },
                    { "Addresses[2].Address.City", viewModel.Addresses[2].Address.City },
                    { "Addresses[2].Address.StateProvinceCode", viewModel.Addresses[2].Address.StateProvinceCode },
                    { "Addresses[2].Address.CountryRegionCode", viewModel.Addresses[2].Address.CountryRegionCode },
                    { "Person.Title", viewModel.Person!.Title },
                    { "Person.Name.FirstName", viewModel.Person.Name!.FirstName },
                    { "Person.Name.MiddleName", viewModel.Person.Name.MiddleName },
                    { "Person.Name.LastName", viewModel.Person.Name.LastName },
                    { "Person.Suffix", viewModel.Person.Suffix },
                    { "Person.PhoneNumbers[0].PhoneNumber", viewModel.Person.PhoneNumbers[0].PhoneNumber },
                    { "Person.PhoneNumbers[0].PhoneNumberType", viewModel.Person.PhoneNumbers[0].PhoneNumberType },
                    { "Person.PhoneNumbers[1].PhoneNumber", viewModel.Person.PhoneNumbers[1].PhoneNumber },
                    { "Person.PhoneNumbers[1].PhoneNumberType", viewModel.Person.PhoneNumbers[1].PhoneNumberType },
                    { "Person.PhoneNumbers[2].PhoneNumber", viewModel.Person.PhoneNumbers[2].PhoneNumber },
                    { "Person.PhoneNumbers[2].PhoneNumberType", viewModel.Person.PhoneNumbers[2].PhoneNumberType },
                    { "email_1", viewModel.Person!.EmailAddresses[0].EmailAddress },
                    { "email_2", viewModel.Person.EmailAddresses[1].EmailAddress },
                    { "email_3", viewModel.Person.EmailAddresses[2].EmailAddress }
                }
            );

            var requestMock = new Mock<HttpRequest>();
            requestMock.SetupGet(x => x.Form).Returns(formData);

            var contextMock = new Mock<HttpContext>();
            contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);

            var bindingContext = new DefaultModelBindingContext
            {
                ActionContext = new ActionContext
                {
                    HttpContext = contextMock.Object
                },
                ModelMetadata = new TestModelMetadata(
                    ModelMetadataIdentity.ForType(typeof(IndividualCustomerViewModel))
                )
            };

            //Act
            await sut.BindModelAsync(bindingContext);

            //Assert
            bindingContext.Result.Model.Should().BeEquivalentTo(viewModel, opt => 
                opt
                    .Excluding(_ => _.SalesOrders)
            );
        }

        [Theory, AutoMoqData]
        public async Task BindModelGivenAddedPhoneNumbers(
            IndividualCustomerViewModelBinder sut,
            IndividualCustomerViewModel viewModel
        )
        {
            //Arrange
            var formData = new FormCollection(
                new Dictionary<string, StringValues>
                {
                    { "AccountNumber", viewModel.AccountNumber },
                    { "CustomerName", viewModel.CustomerName },
                    { "Territory", viewModel.Territory },
                    { "Addresses[0].AddressType", viewModel.Addresses[0].AddressType },
                    { "Addresses[0].Address.AddressLine1", viewModel.Addresses[0].Address.AddressLine1 },
                    { "Addresses[0].Address.AddressLine2", viewModel.Addresses[0].Address.AddressLine2 },
                    { "Addresses[0].Address.PostalCode", viewModel.Addresses[0].Address.PostalCode },
                    { "Addresses[0].Address.City", viewModel.Addresses[0].Address.City },
                    { "Addresses[0].Address.StateProvinceCode", viewModel.Addresses[0].Address.StateProvinceCode },
                    { "Addresses[0].Address.CountryRegionCode", viewModel.Addresses[0].Address.CountryRegionCode },
                    { "Addresses[1].AddressType", viewModel.Addresses[1].AddressType },
                    { "Addresses[1].Address.AddressLine1", viewModel.Addresses[1].Address.AddressLine1 },
                    { "Addresses[1].Address.AddressLine2", viewModel.Addresses[1].Address.AddressLine2 },
                    { "Addresses[1].Address.PostalCode", viewModel.Addresses[1].Address.PostalCode },
                    { "Addresses[1].Address.City", viewModel.Addresses[1].Address.City },
                    { "Addresses[1].Address.StateProvinceCode", viewModel.Addresses[1].Address.StateProvinceCode },
                    { "Addresses[1].Address.CountryRegionCode", viewModel.Addresses[1].Address.CountryRegionCode },
                    { "Addresses[2].AddressType", viewModel.Addresses[2].AddressType },
                    { "Addresses[2].Address.AddressLine1", viewModel.Addresses[2].Address.AddressLine1 },
                    { "Addresses[2].Address.AddressLine2", viewModel.Addresses[2].Address.AddressLine2 },
                    { "Addresses[2].Address.PostalCode", viewModel.Addresses[2].Address.PostalCode },
                    { "Addresses[2].Address.City", viewModel.Addresses[2].Address.City },
                    { "Addresses[2].Address.StateProvinceCode", viewModel.Addresses[2].Address.StateProvinceCode },
                    { "Addresses[2].Address.CountryRegionCode", viewModel.Addresses[2].Address.CountryRegionCode },
                    { "Person.Title", viewModel.Person!.Title },
                    { "Person.Name.FirstName", viewModel.Person.Name!.FirstName },
                    { "Person.Name.MiddleName", viewModel.Person.Name.MiddleName },
                    { "Person.Name.LastName", viewModel.Person.Name.LastName },
                    { "Person.Suffix", viewModel.Person.Suffix },
                    { "Person.EmailAddresses[0].EmailAddress", viewModel.Person.EmailAddresses[0].EmailAddress },
                    { "Person.EmailAddresses[1].EmailAddress", viewModel.Person.EmailAddresses[1].EmailAddress },
                    { "Person.EmailAddresses[2].EmailAddress", viewModel.Person.EmailAddresses[2].EmailAddress },
                    { "phone_1", viewModel.Person!.PhoneNumbers[0].PhoneNumber },
                    { "phone_2", viewModel.Person.PhoneNumbers[1].PhoneNumber },
                    { "phone_3", viewModel.Person.PhoneNumbers[2].PhoneNumber },
                    { "phoneSelect", "test" }
                }
            );

            var requestMock = new Mock<HttpRequest>();
            requestMock.SetupGet(x => x.Form).Returns(formData);

            var contextMock = new Mock<HttpContext>();
            contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);

            var bindingContext = new DefaultModelBindingContext
            {
                ActionContext = new ActionContext
                {
                    HttpContext = contextMock.Object
                },
                ModelMetadata = new TestModelMetadata(
                    ModelMetadataIdentity.ForType(typeof(IndividualCustomerViewModel))
                )
            };

            //Act
            await sut.BindModelAsync(bindingContext);

            //Assert
            bindingContext.Result.Model.Should().BeEquivalentTo(viewModel, opt =>
                opt
                    .Excluding(_ => _.SalesOrders)
                    .Excluding(_ => _.Person!.PhoneNumbers)
            );
        }
    }
}
