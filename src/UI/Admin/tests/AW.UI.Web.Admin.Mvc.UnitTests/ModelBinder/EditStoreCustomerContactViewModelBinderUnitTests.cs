using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer.ModelBinders;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder;

public class EditStoreCustomerContactViewModelBinderUnitTests
{
    [Theory, AutoMoqData]
    public async Task BindModelGivenFormData(
        EditStoreCustomerContactViewModelBinder sut,
        Guid customerId,
        NameFactory name,
        string title,
        string suffix,
        List<string> emailAddresses,
        string contactType
    )
    {
        //Arrange
        var formData = new FormCollection(
            new Dictionary<string, StringValues>
            {
                { "CustomerId", customerId.ToString() },
                { "CustomerContact.ContactPerson.Title", title },
                { "CustomerContact.ContactPerson.Name.FirstName", name.FirstName },
                { "CustomerContact.ContactPerson.Name.MiddleName", name.MiddleName },
                { "CustomerContact.ContactPerson.Name.LastName", name.LastName },
                { "CustomerContact.ContactPerson.Suffix", suffix },
                { "CustomerContact.ContactPerson.EmailAddresses[0].EmailAddress", emailAddresses[0] },
                { "CustomerContact.ContactPerson.EmailAddresses[1].EmailAddress", emailAddresses[1] },
                { "CustomerContact.ContactPerson.EmailAddresses[2].EmailAddress", emailAddresses[2] },
                { "CustomerContact.ContactType", contactType }
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
                ModelMetadataIdentity.ForType(typeof(StoreCustomerContactViewModel))
            )
        };

        //Act
        await sut.BindModelAsync(bindingContext);

        //Assert

        var expected = new EditStoreCustomerContactViewModel
        {
            CustomerId = customerId,
            CustomerContact = new EditStoreCustomerContactContactViewModel
            {
                ContactType = contactType,
                ContactPerson = new EditStoreCustomerContactPersonViewModel
                {
                    Title = title,
                    Name = new NameFactory(name.FirstName!, name.MiddleName, name.LastName!),
                    Suffix = suffix,
                    EmailAddresses = emailAddresses.Select(_ =>
                            new EditStoreCustomerContactPersonEmailAddressViewModel
                            {
                                EmailAddress = _
                            }
                        )
                        .ToList()
                }
            }
        };

        bindingContext.Result.Model.Should().BeEquivalentTo(expected);
    }
}
