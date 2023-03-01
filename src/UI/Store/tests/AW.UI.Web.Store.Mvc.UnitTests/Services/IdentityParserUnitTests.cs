using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Store.Mvc.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Xunit;

namespace AW.UI.Web.Store.Mvc.UnitTests.Services
{
    public class IdentityParserUnitTests
    {
        public class Parse
        {
            [Theory, AutoMoqData]
            public void Parse_ClaimsPrincipal_ReturnsApplicationUser(
                IdentityParser sut,
                Mock<ClaimsPrincipal> mockClaimsPrincipal,
                string subject,
                string lastName,
                string name
            )
            {
                //Arrange
                mockClaimsPrincipal.Setup(_ => _.Claims)
                    .Returns(new List<Claim>
                    {
                        new Claim("sub", subject),
                        new Claim("last_name", lastName),
                        new Claim("name", name)
                    });

                //Act
                var viewModel = sut.Parse(mockClaimsPrincipal.Object);

                //Assert
                viewModel.Id.Should().Be(subject);
                viewModel.LastName.Should().Be(lastName);
                viewModel.Name.Should().Be(name);
            }

            [Theory, AutoMoqData]
            public void Parse_NoClaimsPrincipal_ThrowArgumentException(
                IdentityParser sut,
                Mock<IPrincipal> mockPrincipal
            )
            {
                //Arrange

                //Act
                Action act = () => sut.Parse(mockPrincipal.Object);

                //Assert
                act.Should().Throw<ArgumentException>();
            }
        }
    }
}
