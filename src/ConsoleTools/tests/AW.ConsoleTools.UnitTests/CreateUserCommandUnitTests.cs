using AutoFixture.Xunit2;
using AutoMapper;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.Handlers.AzureAD.CreateUser;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class CreateUserCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserDoesNotExist_CreateUser(
            Mock<Microsoft.Graph.User> mockUser,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<CreateUserCommandHandler>> mockLogger,
            CreateUserCommand command,
            IMapper mapper,
            Mock<IConfiguration> mockConfiguration
        )
        {
            // Arrange
            mockGraphServiceClient.Setup(_ => _.Users
                .Request()
                .AddAsync(
                    It.IsAny<Microsoft.Graph.User>(), 
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(mockUser.Object);

            //Act
            var sut = new CreateUserCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper,
                mockConfiguration.Object
            );
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Id.Should().Be(mockUser.Object.Id);
            result.DisplayName.Should().Be(mockUser.Object.DisplayName);

            mockGraphServiceClient.Verify(_ => _.Users
                .Request()
                .AddAsync(
                    It.IsAny<Microsoft.Graph.User>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }
}