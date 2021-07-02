using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomers;
using AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetAddressTypes;
using AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetCountries;
using AW.UI.Web.Common.ApiClients.ReferenceDataApi.Models.GetStateProvinces;
using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.Internal.Services;
using AW.UI.Web.Internal.ViewModels.Customer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests
{
    public class CustomerControllerUnitTests
    {
        public class Index
        {
            [Fact]
            public async void Index_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomers(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<CustomerType?>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new CustomersIndexViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.Index(
                    0, null, null, null
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<CustomersIndexViewModel>();
            }
        }
        
        public class Detail
        {
            [Fact]
            public async void Detail_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomer(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new CustomerDetailViewModel
                {
                    Customer = new StoreCustomerViewModel
                    {
                        AccountNumber = "AW00000001",
                        CustomerName = "A Bike Store"
                    }
                }
                );

                var countries = new List<CountryRegion>
                {
                    new CountryRegion { CountryRegionCode = "US", Name = "United States" },
                    new CountryRegion { CountryRegionCode = "GB", Name = "United Kingdom" }
                };
                var statesProvinces = new List<StateProvince>
                {
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "NY", Name = "New York" },
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
                };

                var mockReferenceDataService = new Mock<IReferenceDataService>();
                mockReferenceDataService.Setup(x => x.GetCountries())
                    .ReturnsAsync(countries);
                mockReferenceDataService.Setup(x => x.GetStatesProvinces(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.Detail("AW00000001");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                var viewModel = viewResult.Model.Should().BeAssignableTo<CustomerDetailViewModel>().Subject;

                viewResult.ViewData["accountNumber"].Should().Be("AW00000001");
                viewResult.ViewData["customerName"] = viewModel.Customer.CustomerName;
                viewResult.ViewData["countries"].Should().Be(countries);
                viewResult.ViewData["statesProvinces"].Should().Be(statesProvinces);
            }
        }

        public class EditStore
        {
            [Fact]
            public async void EditStoreGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetStoreCustomerForEdit(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new EditStoreCustomerViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.EditStore("AW00000001");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditStoreCustomerViewModel>();
            }

            [Fact]
            public async void EditStorePost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditStoreCustomerViewModel
                {
                    Customer = new StoreCustomerViewModel
                    {
                        AccountNumber = "AW00000001"
                    }
                };
                var actionResult = await controller.EditStore(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void EditStorePost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditStoreCustomerViewModel();
                var actionResult = await controller.EditStore(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditStoreCustomerViewModel>();
            }
        }

        public class EditIndividual
        {
            [Fact]
            public async void EditIndividualGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetIndividualCustomerForEdit(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new EditIndividualCustomerViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.EditIndividual("AW00000001");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditIndividualCustomerViewModel>();
            }

            [Fact]
            public async void EditIndividualPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditIndividualCustomerViewModel
                {
                    Customer = new IndividualCustomerViewModel
                    {
                        AccountNumber = "AW00000001"
                    }
                };
                var actionResult = await controller.EditIndividual(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void EditIndividualPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditIndividualCustomerViewModel();
                var actionResult = await controller.EditIndividual(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditIndividualCustomerViewModel>();
            }
        }

        public class AddAddress
        {
            [Fact]
            public async void AddAddressGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.AddAddress(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ))
                .Returns(new EditCustomerAddressViewModel());

                var addressTypes = new List<AddressType>
                {
                    new AddressType { Name = "Home" },
                    new AddressType { Name = "Main Office" }
                };
                var addressTypesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "Home", Text = "Home" },
                    new SelectListItem { Value = "Main Office", Text = "Main Office" }
                };
                var countries = new List<CountryRegion>
                {
                    new CountryRegion { CountryRegionCode = "US", Name = "United States" },
                    new CountryRegion { CountryRegionCode = "GB", Name = "United Kingdom" }
                };
                var countriesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "US", Text = "United States" },
                    new SelectListItem { Value = "GB", Text = "United Kingdom" }
                };
                var statesProvinces = new List<StateProvince>
                {
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "NY", Name = "New York" },
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
                };
                var statesProvincesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "CA", Text = "California" }
                };

                var mockReferenceDataService = new Mock<IReferenceDataService>();
                mockReferenceDataService.Setup(x => x.GetAddressTypes())
                    .ReturnsAsync(addressTypes);
                mockReferenceDataService.Setup(x => x.GetCountries())
                    .ReturnsAsync(countries);
                mockReferenceDataService.Setup(x => x.GetStatesProvinces(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.AddAddress("AW00000001", "A Bike Store");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerAddressViewModel>();

                viewResult.ViewData["addressTypes"].Should().BeEquivalentTo(addressTypesList);
                viewResult.ViewData["countries"].Should().BeEquivalentTo(countriesList);
                viewResult.ViewData["statesProvinces"].Should().BeEquivalentTo(statesProvincesList);
            }

            [Fact]
            public async void AddAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditCustomerAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.AddAddress(viewModel);

                //Assert
                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void AddAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditCustomerAddressViewModel();
                var actionResult = await controller.AddAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerAddressViewModel>();
            }
        }

        public class EditAddress
        {
            [Fact]
            public async void EditAddress_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomerAddress(
                    It.IsAny<string>(),
                    It.IsAny<string>()
                ))
                .ReturnsAsync(new EditCustomerAddressViewModel
                {
                    CustomerAddress = new CustomerAddressViewModel
                    {
                        AddressType = "Main Office",
                        Address = new AddressViewModel
                        {
                            AddressLine1 = "2251 Elliot Avenue",
                            PostalCode = "98104",
                            City = "Seattle",
                            StateProvinceCode = "WA",
                            CountryRegionCode = "US"
                        }
                    }
                }
                );

                var addressTypes = new List<AddressType>
                {
                    new AddressType { Name = "Home" },
                    new AddressType { Name = "Main Office" }
                };
                var addressTypesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "Home", Text = "Home" },
                    new SelectListItem { Value = "Main Office", Text = "Main Office" }
                };
                var countries = new List<CountryRegion>
                {
                    new CountryRegion { CountryRegionCode = "US", Name = "United States" },
                    new CountryRegion { CountryRegionCode = "GB", Name = "United Kingdom" }
                };
                var countriesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "US", Text = "United States" },
                    new SelectListItem { Value = "GB", Text = "United Kingdom" }
                };
                var statesProvinces = new List<StateProvince>
                {
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "NY", Name = "New York" },
                    new StateProvince { CountryRegionCode = "US", StateProvinceCode = "CA", Name = "California" }
                };
                var statesProvincesList = new List<SelectListItem>
                {
                    new SelectListItem { Value = "", Text = "--Select--", Selected = true },
                    new SelectListItem { Value = "NY", Text = "New York" },
                    new SelectListItem { Value = "CA", Text = "California" }
                };

                var mockReferenceDataService = new Mock<IReferenceDataService>();
                mockReferenceDataService.Setup(x => x.GetAddressTypes())
                    .ReturnsAsync(addressTypes);
                mockReferenceDataService.Setup(x => x.GetCountries())
                    .ReturnsAsync(countries);
                mockReferenceDataService.Setup(x => x.GetStatesProvinces(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.EditAddress("AW00000001", "Main Office");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                var viewModel = viewResult.Model.Should().BeAssignableTo<EditCustomerAddressViewModel>().Subject;

                viewResult.ViewName.Should().Be("Address");
                viewResult.ViewData["addressTypes"].Should().BeEquivalentTo(addressTypesList);
                viewResult.ViewData["countries"].Should().BeEquivalentTo(countriesList);
                viewResult.ViewData["statesProvinces"].Should().BeEquivalentTo(statesProvincesList);
            }

            [Fact]
            public async void EditAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditCustomerAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.EditAddress(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.UpdateAddress(It.IsAny<EditCustomerAddressViewModel>()));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void EditAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditCustomerAddressViewModel();
                var actionResult = await controller.EditAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerAddressViewModel>();
            }
        }

        public class GetStatesProvinces
        {
            [Fact]
            public async void GetStatesProvinces_ReturnsJsonResult()
            {
                //Arrange
                var statesProvinces = new List<StateProvinceViewModel>
                {
                    new StateProvinceViewModel { CountryRegionCode = "NY" },
                    new StateProvinceViewModel { CountryRegionCode = "CA" }
                };

                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetStatesProvincesJson(It.IsAny<string>()))
                    .ReturnsAsync(statesProvinces);

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var jsonResult = await controller.GetStatesProvinces("US");

                //Assert
                jsonResult.Value.Should().Be(statesProvinces);
            }
        }

        public class DeleteAddress
        {
            [Fact]
            public async void DeleteAddressGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomerAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new DeleteCustomerAddressViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.DeleteAddress("AW00000001", "Main Office");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteCustomerAddressViewModel>();

                mockCustomerService.Verify(x => x.GetCustomerAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void DeleteAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new DeleteCustomerAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteAddress(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.DeleteAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void DeleteAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new DeleteCustomerAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteCustomerAddressViewModel>();
            }
        }

        public class AddContact
        {
            [Fact]
            public async void AddContactGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.AddContact(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new EditCustomerContactViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.AddContact("AW00000001", "A Bike Store");

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();

                mockCustomerService.Verify(x => x.AddContact(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void AddContactPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.AddContact(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.AddContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void AddContactPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.AddContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();
            }
        }

        public class EditContact
        {
            [Fact]
            public async void EditContactGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new EditCustomerContactViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.EditContact(
                    "AW00000001",
                    "Orlando N. Gee",
                    "Owner"
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();

                mockCustomerService.Verify(x => x.GetCustomerContact(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void EditContactPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.EditContact(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.UpdateContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void EditContactPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.EditContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();
            }
        }

        public class DeleteContact
        {
            [Fact]
            public async void DeleteContactGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetCustomerContactForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new DeleteCustomerContactViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.DeleteContact(
                    "AW00000001",
                    "Orlando N. Gee",
                    "Owner"
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteCustomerContactViewModel>();

                mockCustomerService.Verify(x => x.GetCustomerContactForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void DeleteContactPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new DeleteCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteContact(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.DeleteContact(
                    It.IsAny<DeleteCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void DeleteContactPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new DeleteCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteCustomerContactViewModel>();
            }
        }

        public class AddContactEmailAddress
        {
            [Fact]
            public void AddContactEmailAddressGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.AddEmailAddress(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .Returns(new EditEmailAddressViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = controller.AddContactEmailAddress(
                    "AW00000001",
                    "A Bike Store"
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditEmailAddressViewModel>();

                mockCustomerService.Verify(x => x.AddEmailAddress(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void AddContactEmailAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.AddContact(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.AddContact(
                    It.IsAny<EditCustomerContactViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void AddContactEmailAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new EditCustomerContactViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.AddContact(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<EditCustomerContactViewModel>();
            }
        }

        public class DeleteIndividualCustomerEmailAddress
        {
            [Fact]
            public async void DeleteIndividualCustomerEmailAddressGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetIndividualCustomerEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new DeleteIndividualCustomerEmailAddressViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.DeleteIndividualCustomerEmailAddress(
                    "AW00011000",
                    "jon24@adventure-works.com"
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteIndividualCustomerEmailAddressViewModel>();

                mockCustomerService.Verify(x => x.GetIndividualCustomerEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>())
                );
            }

            [Fact]
            public async void DeleteIndividualCustomerEmailAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new DeleteIndividualCustomerEmailAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteIndividualCustomerEmailAddress(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.DeleteIndividualCustomerEmailAddress(
                    It.IsAny<DeleteIndividualCustomerEmailAddressViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void DeleteIndividualCustomerEmailAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new DeleteIndividualCustomerEmailAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteIndividualCustomerEmailAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteIndividualCustomerEmailAddressViewModel>();
            }
        }

        public class DeleteContactEmailAddress
        {
            [Fact]
            public async void DeleteContactEmailAddressGet_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                mockCustomerService.Setup(x => x.GetContactEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ))
                .ReturnsAsync(new DeleteContactEmailAddressViewModel());

                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var actionResult = await controller.DeleteContactEmailAddress(
                    "AW00000001",
                    "Owner",
                    "Orlando N. Gee",
                    "orlando0@adventure-works.com"
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteContactEmailAddressViewModel>();

                mockCustomerService.Verify(x => x.GetContactEmailAddressForDelete(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()
                ));
            }

            [Fact]
            public async void DeleteContactEmailAddressPost_ValidModelState_ReturnsRedirect()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );

                //Act
                var viewModel = new DeleteContactEmailAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteContactEmailAddress(viewModel);

                //Assert
                mockCustomerService.Verify(x => x.DeleteContactEmailAddress(
                    It.IsAny<DeleteContactEmailAddressViewModel>()
                ));

                var redirectResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                redirectResult.ActionName.Should().Be("Detail");
                redirectResult.RouteValues.Count.Should().Be(1);
                redirectResult.RouteValues.Keys.Contains("AccountNumber");
                redirectResult.RouteValues.Values.Contains("AW00000001");
            }

            [Fact]
            public async void DeleteContactEmailAddressPost_InvalidModelState_ReturnsViewModel()
            {
                //Arrange
                var mockCustomerService = new Mock<ICustomerService>();
                var mockReferenceDataService = new Mock<IReferenceDataService>();

                var controller = new CustomerController(
                    mockCustomerService.Object,
                    mockReferenceDataService.Object
                );
                controller.ModelState.AddModelError("AccountNumber", "AW00000001");

                //Act
                var viewModel = new DeleteContactEmailAddressViewModel
                {
                    AccountNumber = "AW00000001"
                };
                var actionResult = await controller.DeleteContactEmailAddress(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeAssignableTo<DeleteContactEmailAddressViewModel>();
            }
        }
    }
}