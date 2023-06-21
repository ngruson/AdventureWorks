using AW.UI.Web.Admin.Mvc.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers;

public class AccountControllerUnitTests
{
    [Fact]
    public void Signout_ReturnsHomeViewModel()
    {
        //Arrange
        var mockLogger = new Mock<ILogger<AccountController>>();

        //Act
        var controller = new AccountController();
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
        var actionResult = controller.Logout();

        //Assert
        var signOutResult = actionResult.Should().BeAssignableTo<SignOutResult>().Subject;
        signOutResult.AuthenticationSchemes.Should().BeEquivalentTo(new string[] { "Cookies", "oidc" });
    }
}
