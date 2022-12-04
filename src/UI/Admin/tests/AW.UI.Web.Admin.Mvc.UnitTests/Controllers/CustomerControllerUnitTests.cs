using AutoFixture.Xunit2;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Customer;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetCountries;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetStatesProvinces;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class CustomerControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                CustomersIndexViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomers(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CustomerType?>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.Index(
                    0, null, null, null
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class Detail
        {
            [Theory, AutoMoqData]
            public async Task Detail_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                CustomerViewModel viewModel,
                List<SelectListItem> addressTypes,
                List<SelectListItem> countries,
                List<SelectListItem> salesPersons,
                List<SelectListItem> statesProvinces,
                List<SelectListItem> territories,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(_ => _.GetCustomer(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                customerService.Setup(_ => _.GetAddressTypes())
                    .ReturnsAsync(addressTypes);

                customerService.Setup(_ => _.GetCountries())
                    .ReturnsAsync(countries);

                customerService.Setup(_ => _.GetSalesPersons(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(salesPersons);

                customerService.Setup(_ => _.GetStatesProvinces(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(statesProvinces);

                customerService.Setup(_ => _.GetTerritories())
                    .ReturnsAsync(territories);

                //Act
                var actionResult = await sut.Detail(viewModel.AccountNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                var result = viewResult.Model.Should().Be(viewModel);

                viewResult.ViewData["accountNumber"].Should().Be(viewModel.AccountNumber);
                viewResult.ViewData["customerName"] = viewModel.CustomerName;

                viewResult.ViewData["addressTypes"].Should().Be(addressTypes);
                viewResult.ViewData["countries"].Should().Be(countries);
                viewResult.ViewData["salesPersons"].Should().Be(salesPersons);
                viewResult.ViewData["statesProvinces"].Should().Be(statesProvinces);
                viewResult.ViewData["territories"].Should().Be(territories);
            }
        }

        public class EditStore
        {
            [Theory, AutoMoqData]
            public async Task EditStoreGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditStoreCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetStoreCustomerForEdit(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.EditStore(viewModel.Customer.AccountNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }

            [Theory, AutoMoqData]
            public async Task EditStorePost_ValidModelState_ReturnsRedirect(
                EditStoreCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.EditStore(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.Customer.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async Task EditStorePost_InvalidModelState_ReturnsViewModel(
                EditStoreCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.EditStore(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class EditIndividual
        {
            [Theory, AutoMoqData]
            public async Task EditIndividualGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditIndividualCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetIndividualCustomerForEdit(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.EditIndividual("AW00000001");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }

            [Theory, AutoMoqData]
            public async Task EditIndividualPost_ValidModelState_ReturnsRedirect(
                EditIndividualCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.EditIndividual(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.Customer.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async Task EditIndividualPost_InvalidModelState_ReturnsViewModel(
                EditIndividualCustomerViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.EditIndividual(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class AddAddress
        {
            [Theory, AutoMoqData]
            public async Task AddAddressPost_ValidModelState_ReturnsRedirect(
                CustomerAddressViewModel viewModel,
                string accountNumber,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.AddAddress(viewModel, accountNumber);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(accountNumber);
            }

            [Theory, AutoMoqData]
            public async Task AddAddressPost_InvalidModelState_ReturnsViewModel(
                CustomerAddressViewModel viewModel,
                string accountNumber,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", accountNumber);

                //Act
                var actionResult = await sut.AddAddress(viewModel, accountNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class GetStatesProvinces
        {
            [Theory, AutoMoqData]
            public async Task GetStatesProvinces_ReturnsJsonResult(
                [Frozen] Mock<ICustomerService> customerService,
                List<StateProvince> statesProvinces,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetStatesProvincesJson(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                //Act
                var jsonResult = await sut.GetStatesProvinces("US");

                //Assert
                jsonResult.Value.Should().Be(statesProvinces);
            }
        }

        public class DeleteAddress
        {
            [Theory, AutoMoqData]
            public async Task DeleteAddress_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                string accountNumber,
                string addressType,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteAddress(accountNumber, addressType);

                //Assert
                customerService.Verify(x => x.DeleteAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(accountNumber);
            }

            [Theory, AutoMoqData]
            public async Task DeleteAddress_InvalidModelState_ReturnsViewModel(
                DeleteCustomerAddressViewModel viewModel,
                string accountNumber,
                string addressType,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", accountNumber);

                //Act
                var actionResult = await sut.DeleteAddress(accountNumber, addressType);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(accountNumber);
            }
        }

        public class AddStoreCustomerContact
        {
            [Theory, AutoMoqData]
            public async Task AddContactGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.AddContact(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.AddStoreCustomerContact(
                    viewModel.AccountNumber,
                    viewModel.CustomerName
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.AddContact(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async Task AddContactPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut,
                List<string> newEmail
            )
            {
                //Act
                var actionResult = await sut.AddStoreCustomerContact(viewModel, newEmail);

                //Assert
                customerService.Verify(x => x.AddContact(
                    It.IsAny<StoreCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("StoreCustomerContact");
                redirectResult.RouteValues.Count.Should().Be(2);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.ContainsKey("ContactName");
                var values = redirectResult.RouteValues.Values.ToList();
                values[0].Should().Be(viewModel.AccountNumber);
                values[1].Should().Be(viewModel.CustomerContact.ContactPerson.Name.FullName);
            }

            [Theory, AutoMoqData]
            public async Task AddContactPost_InvalidModelState_ReturnsViewModel(
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut,
                List<string> newEmail
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.AddStoreCustomerContact(viewModel, newEmail);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class StoreCustomerContact
        {
            [Theory, AutoMoqData]
            public async Task EditContactGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.StoreCustomerContact(
                    viewModel.AccountNumber,
                    viewModel.CustomerContact.ContactPerson.Name.FullName
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<StoreCustomerContactViewModel>();

                customerService.Verify(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>()
                ));
            }

            [Theory, AutoMoqData]
            public async Task EditContactPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut,
                List<string> email
            )
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();


                //Act
                var actionResult = await sut.StoreCustomerContact(viewModel, email);

                //Assert
                customerService.Verify(x => x.UpdateContact(
                    It.IsAny<StoreCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("StoreCustomerContact");
                redirectResult.RouteValues.Count.Should().Be(2);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.ContainsKey("ContactName");
                var values = redirectResult.RouteValues.Values.ToList();
                values[0].Should().Be(viewModel.AccountNumber);
                values[1].Should().Be(viewModel.CustomerContact.ContactPerson.Name.FullName);
            }

            [Theory, AutoMoqData]
            public async Task EditContactPost_InvalidModelState_ReturnsViewModel(
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut,
                List<string> email
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", viewModel.AccountNumber);

                //Act
                var actionResult = await sut.StoreCustomerContact(viewModel, email);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("StoreCustomerContact");
                redirectResult.RouteValues.Count.Should().Be(2);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.ContainsKey("ContactName");
                var values = redirectResult.RouteValues.Values.ToList();
                values[0].Should().Be(viewModel.AccountNumber);
                values[1].Should().Be(viewModel.CustomerContact.ContactPerson.Name.FullName);
            }
        }

        public class DeleteContact
        {
            [Theory, AutoMoqData]
            public async Task DeleteContact_ReturnsViewModel(
                DeleteCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteContact(
                    viewModel.AccountNumber,
                    viewModel.ContactPerson.Name.FullName
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be("Detail");
                viewResult.RouteValues.Should().ContainKey("accountNumber");
            }
        }

        public class AddContactEmailAddress
        {
            [Theory, AutoMoqData]
            public void AddContactEmailAddressGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.AddEmailAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .Returns(viewModel);

                //Act
                var actionResult = sut.AddContactEmailAddress(
                    viewModel.AccountNumber,
                    viewModel.PersonName
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.AddEmailAddress(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async Task AddContactEmailAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                StoreCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.AddStoreCustomerContact(
                    viewModel, 
                    new List<string>()
                );

                //Assert
                customerService.Verify(x => x.AddContact(
                    It.IsAny<StoreCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("StoreCustomerContact");
                redirectResult.RouteValues.Count.Should().Be(2);
                redirectResult.RouteValues.Should().ContainKey("AccountNumber");
                redirectResult.RouteValues.Should().ContainValue(viewModel.AccountNumber);
                redirectResult.RouteValues.Should().ContainKey("ContactName");
                redirectResult.RouteValues.Should().ContainValue(viewModel.CustomerContact.ContactPerson.Name.FullName);
            }

            [Theory, AutoMoqData]
            public async Task AddContactEmailAddressPost_InvalidModelState_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.AddEmailAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .Returns(viewModel);

                sut.ModelState.AddModelError("AccountNumber", viewModel.AccountNumber);

                //Act
                var actionResult = await sut.AddContactEmailAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class DeleteIndividualCustomerEmailAddress
        {
            [Theory, AutoMoqData]
            public async Task DeleteIndividualCustomerEmailAddressGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteIndividualCustomerEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetIndividualCustomerEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.DeleteIndividualCustomerEmailAddress(
                    viewModel.AccountNumber,
                    viewModel.EmailAddress
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.GetIndividualCustomerEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async Task DeleteIndividualCustomerEmailAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteIndividualCustomerEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteIndividualCustomerEmailAddress(viewModel);

                //Assert
                customerService.Verify(x => x.DeleteIndividualCustomerEmailAddress(
                    It.IsAny<DeleteIndividualCustomerEmailAddressViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.ContainsKey("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async Task DeleteIndividualCustomerEmailAddressPost_InvalidModelState_ReturnsViewModel(
                DeleteIndividualCustomerEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", viewModel.AccountNumber);

                //Act
                var actionResult = await sut.DeleteIndividualCustomerEmailAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class DeleteContactEmailAddress
        {
            [Theory, AutoMoqData]
            public async Task DeleteContactEmailAddress_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                string accountNumber,
                string contactName,
                string emailAddress,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteContactEmailAddress(
                    accountNumber,
                    contactName,
                    emailAddress
                );

                //Assert
                customerService.Verify(x => x.DeleteContactEmailAddress(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("StoreCustomerContact");
                redirectResult.RouteValues.Count.Should().Be(2);
                redirectResult.RouteValues.ContainsKey("AccountNumber").Should().BeTrue();
                redirectResult.RouteValues.Values.Contains(accountNumber).Should().BeTrue();
                redirectResult.RouteValues.ContainsKey("ContactName").Should().BeTrue();
                redirectResult.RouteValues.Values.Contains(contactName).Should().BeTrue();
            }
        }
    }
}