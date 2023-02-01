﻿using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.ValueTypes;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using FluentAssertions;

namespace AW.UI.Web.Admin.Mvc.UnitTests.ModelBinder
{
    public class UpdateOrderlinesViewModelBinderUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Test(
            UpdateOrderlinesViewModelBinder sut,
            string accountNumber,
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
                    { "AccountNumber", accountNumber },
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
                    ModelMetadataIdentity.ForType(typeof(UpdateOrderlinesViewModel))
                )
            };

            //Act
            await sut.BindModelAsync(bindingContext);

            //Assert

            var expected = new StoreCustomerContactViewModel
            {
                AccountNumber = accountNumber,
                CustomerContact = new CustomerContactViewModel
                {
                    ContactType = contactType,
                    ContactPerson = new PersonViewModel
                    {
                        Title = title,
                        Name = new PersonNameViewModel
                        {
                            FirstName = name.FirstName,
                            MiddleName = name.MiddleName,
                            LastName = name.LastName
                        },
                        Suffix = suffix,
                        EmailAddresses = emailAddresses.Select(_ =>
                                new PersonEmailAddressViewModel
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
}