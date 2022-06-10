using AutoFixture.Xunit2;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.Services.IdentityServer.Core.Models;
using AW.SharedKernel.UnitTesting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.IdentityServer.Core.UnitTests
{
    public class CreateLoginCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_UserDoesExist_CreateLogin(
            Mock<UserManager<ApplicationUser>> userManagerMock,
            CreateLoginCommandHandler sut,
            CreateLoginCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            userManagerMock.Verify(_ => _.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                ), 
                Times.Never()
            );

            userManagerMock.Verify(_ => _.AddClaimsAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<IEnumerable<Claim>>()
                ),
                Times.Never()
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_UserDoesNotExist_CreateUserSucceeds_CreateLoginAndClaims(
            [Frozen] Mock<UserManager<ApplicationUser>> userManagerMock,
            Mock<ILogger<CreateLoginCommandHandler>> loggerMock,
            Mock<IConfiguration> configurationMock,
            CreateLoginCommand command,
            ApplicationUser? applicationUser
        )
        {
            //Arrange
            applicationUser = null;
            userManagerMock.Setup(_ => _.FindByNameAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(applicationUser!);

            userManagerMock.Setup(_ => _.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(_ => _.AddClaimsAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<IEnumerable<Claim>>()
                )
            )
            .ReturnsAsync(IdentityResult.Success);

            var sut = new CreateLoginCommandHandler(
                loggerMock.Object,
                userManagerMock.Object,
                configurationMock.Object
            );

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            
            userManagerMock.Verify(_ => _.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                ),
                Times.Once()
            );

            userManagerMock.Verify(_ => _.AddClaimsAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<IEnumerable<Claim>>()
                ),
                Times.Once()
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_UserDoesNotExist_CreateUserFails_CreateLoginAndClaims(
            [Frozen] Mock<UserManager<ApplicationUser>> userManagerMock,
            Mock<ILogger<CreateLoginCommandHandler>> loggerMock,
            Mock<IConfiguration> configurationMock,
            CreateLoginCommand command,
            ApplicationUser? applicationUser
        )
        {
            //Arrange
            applicationUser = null;
            userManagerMock.Setup(_ => _.FindByNameAsync(
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(applicationUser!);

            userManagerMock.Setup(_ => _.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                )
            )
            .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

            var sut = new CreateLoginCommandHandler(
                loggerMock.Object,
                userManagerMock.Object,
                configurationMock.Object
            );

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert

            userManagerMock.Verify(_ => _.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<string>()
                ),
                Times.Once()
            );

            userManagerMock.Verify(_ => _.AddClaimsAsync(
                    It.IsAny<ApplicationUser>(),
                    It.IsAny<IEnumerable<Claim>>()
                ),
                Times.Never()
            );
        }
    }
}