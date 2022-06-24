using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Controllers
{
    public class AccountControllerUnitTests
    {
        [Theory, MockHttpData]
        public async Task SignIn_ReturnsHomeViewModel(
            string returnUrl,
            string accessToken,
            Mock<IApplication> mockApplication
        )
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AccountController>>();

            //Act
            var controller = new AccountController(mockApplication.Object, mockLogger.Object);

            var authResult = AuthenticateResult.Success(
                new AuthenticationTicket(new ClaimsPrincipal(), null)
            );

            authResult.Properties.StoreTokens(new[]
            {
                new AuthenticationToken { Name = "access_token", Value = accessToken }
            });

            var mockAuthenticationService = new Mock<IAuthenticationService>();
            mockAuthenticationService
                .Setup(x => x.AuthenticateAsync(It.IsAny<HttpContext>(), null))
                .ReturnsAsync(authResult);

            var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(mockAuthenticationService.Object);
            serviceProvider.Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
                .Returns(mockUrlHelperFactory.Object);

            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };
            var actionResult = await controller.SignIn(returnUrl);

            //Assert
            var redirectToActionResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
            redirectToActionResult.ControllerName.Should().Be("Home");
            redirectToActionResult.ActionName.Should().Be("Index");
        }

        [Theory, MockHttpData]
        public async Task Signout_ReturnsHomeViewModel(Mock<IApplication> mockApplication)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<AccountController>>();

            //Act
            var controller = new AccountController(mockApplication.Object, mockLogger.Object);
            var mockAuthenticationService = new Mock<IAuthenticationService>();

            var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelperFactory.Setup(_ => _.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(mockUrlHelper.Object);

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(mockAuthenticationService.Object);
            serviceProvider.Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
                .Returns(mockUrlHelperFactory.Object);

            controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };
            var actionResult = await controller.Signout();

            //Assert
            var signOutResult = actionResult.Should().BeAssignableTo<SignOutResult>().Subject;
            signOutResult.AuthenticationSchemes.Should().BeEquivalentTo(new string[] { "cookie" });
        }
    }
}