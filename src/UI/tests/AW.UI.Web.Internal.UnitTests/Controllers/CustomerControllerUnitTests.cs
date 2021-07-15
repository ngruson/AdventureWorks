using AutoFixture.Xunit2;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Infrastructure.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.ViewModels.Customer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Controllers
{
    public class CustomerControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async void Index_ReturnsViewModel(
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
            public async void Detail_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                CustomerDetailViewModel viewModel,
                [Frozen] Mock<IReferenceDataService> referenceDataService,
                List<CountryRegion> countries,
                List<StateProvince> statesProvinces,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomer(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                referenceDataService.Setup(x => x.GetCountriesAsync())
                    .ReturnsAsync(countries);
                referenceDataService.Setup(x => x.GetStatesProvincesAsync(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                //Act
                var actionResult = await sut.Detail(viewModel.Customer.AccountNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                var result = viewResult.Model.Should().Be(viewModel);

                viewResult.ViewData["accountNumber"].Should().Be(viewModel.Customer.AccountNumber);
                viewResult.ViewData["customerName"] = viewModel.Customer.CustomerName;
                viewResult.ViewData["countries"].Should().Be(countries);
                viewResult.ViewData["statesProvinces"].Should().Be(statesProvinces);
            }
        }

        public class EditStore
        {
            [Theory, AutoMoqData]
            public async void EditStoreGet_ReturnsViewModel(
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
            public async void EditStorePost_ValidModelState_ReturnsRedirect(
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
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.Customer.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void EditStorePost_InvalidModelState_ReturnsViewModel(
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
            public async void EditIndividualGet_ReturnsViewModel(
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
            public async void EditIndividualPost_ValidModelState_ReturnsRedirect(
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
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.Customer.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void EditIndividualPost_InvalidModelState_ReturnsViewModel(
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
            public async void AddAddressGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.AddAddress(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ))
                .Returns(viewModel);

                //Act
                var actionResult = await sut.AddAddress(
                    viewModel.AccountNumber, 
                    viewModel.CustomerName
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                viewResult.ViewData["addressTypes"].Should().NotBeNull();
                viewResult.ViewData["countries"].Should().NotBeNull();
                viewResult.ViewData["statesProvinces"].Should().NotBeNull();
            }

            [Theory, AutoMoqData]
            public async void AddAddressPost_ValidModelState_ReturnsRedirect(
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.AddAddress(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void AddAddressPost_InvalidModelState_ReturnsViewModel(
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", viewModel.AccountNumber);

                //Act
                var actionResult = await sut.AddAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class EditAddress
        {
            [Theory, AutoMoqData]
            public async void EditAddress_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomerAddress(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.EditAddress("AW00000001", "Main Office");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                viewResult.ViewName.Should().Be("Address");
                viewResult.ViewData["addressTypes"].Should().NotBeNull();
                viewResult.ViewData["countries"].Should().NotBeNull();
                viewResult.ViewData["statesProvinces"].Should().NotBeNull();
            }

            [Theory, AutoMoqData]
            public async void EditAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.EditAddress(viewModel);

                //Assert
                customerService.Verify(x => x.UpdateAddress(It.IsAny<EditCustomerAddressViewModel>()));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void EditAddressPost_InvalidModelState_ReturnsViewModel(
                EditCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act                
                var actionResult = await sut.EditAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class GetStatesProvinces
        {
            [Theory, AutoMoqData]
            public async void GetStatesProvinces_ReturnsJsonResult(
                [Frozen] Mock<ICustomerService> customerService,
                List<StateProvinceViewModel> statesProvinces,
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
            public async void DeleteAddressGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomerAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.DeleteAddress(
                    viewModel.AccountNumber, 
                    viewModel.AddressType
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.GetCustomerAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async void DeleteAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteAddress(viewModel);

                //Assert
                customerService.Verify(x => x.DeleteAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void DeleteAddressPost_InvalidModelState_ReturnsViewModel(
                DeleteCustomerAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.DeleteAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class AddContact
        {
            [Theory, AutoMoqData]
            public async void AddContactGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.AddContact(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.AddContact(
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
            public async void AddContactPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.AddContact(viewModel);

                //Assert
                customerService.Verify(x => x.AddContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void AddContactPost_InvalidModelState_ReturnsViewModel(
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.AddContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class EditContact
        {
            [Theory, AutoMoqData]
            public async void EditContactGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.EditContact(
                    viewModel.AccountNumber,
                    viewModel.CustomerContact.ContactPerson.FullName(),
                    viewModel.CustomerContact.ContactType
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();

                customerService.Verify(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async void EditContactPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();


                //Act
                var actionResult = await sut.EditContact(viewModel);

                //Assert
                customerService.Verify(x => x.UpdateContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void EditContactPost_InvalidModelState_ReturnsViewModel(
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", viewModel.AccountNumber);

                //Act
                var actionResult = await sut.EditContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class DeleteContact
        {
            [Theory, AutoMoqData]
            public async void DeleteContactGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetCustomerContactForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.DeleteContact(
                    viewModel.AccountNumber,
                    viewModel.ContactPerson.FullName(),
                    viewModel.ContactType
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.GetCustomerContactForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Theory, AutoMoqData]
            public async void DeleteContactPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteContact(viewModel);

                //Assert
                customerService.Verify(x => x.DeleteContact(
                    It.IsAny<DeleteCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void DeleteContactPost_InvalidModelState_ReturnsViewModel(
                DeleteCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.DeleteContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
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
            public async void AddContactEmailAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                EditCustomerContactViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.AddContact(viewModel);

                //Assert
                customerService.Verify(x => x.AddContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void AddContactEmailAddressPost_InvalidModelState_ReturnsViewModel(
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
            public async void DeleteIndividualCustomerEmailAddressGet_ReturnsViewModel(
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
            public async void DeleteIndividualCustomerEmailAddressPost_ValidModelState_ReturnsRedirect(
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
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void DeleteIndividualCustomerEmailAddressPost_InvalidModelState_ReturnsViewModel(
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
            public async void DeleteContactEmailAddressGet_ReturnsViewModel(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteContactEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                customerService.Setup(x => x.GetContactEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.DeleteContactEmailAddress(
                    viewModel.AccountNumber,
                    viewModel.ContactType,
                    viewModel.ContactName,
                    viewModel.EmailAddress
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);

                customerService.Verify(x => x.GetContactEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ));
            }

            [Theory, AutoMoqData]
            public async void DeleteContactEmailAddressPost_ValidModelState_ReturnsRedirect(
                [Frozen] Mock<ICustomerService> customerService,
                DeleteContactEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Act
                var actionResult = await sut.DeleteContactEmailAddress(viewModel);

                //Assert
                customerService.Verify(x => x.DeleteContactEmailAddress(
                    It.IsAny<DeleteContactEmailAddressViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains(viewModel.AccountNumber);
            }

            [Theory, AutoMoqData]
            public async void DeleteContactEmailAddressPost_InvalidModelState_ReturnsViewModel(
                DeleteContactEmailAddressViewModel viewModel,
                [Greedy] CustomerController sut
            )
            {
                //Arrange
                sut.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var actionResult = await sut.DeleteContactEmailAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }
    }
}