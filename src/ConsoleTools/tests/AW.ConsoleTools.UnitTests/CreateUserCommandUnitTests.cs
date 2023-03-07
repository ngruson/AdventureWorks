using AutoFixture.Xunit2;
using AutoMapper;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.Handlers.AzureAD.CreateUser;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using Moq;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class CreateUserCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserDoesNotExist_CreateUser(            
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            string id,
            string displayName,
            Mock<ILogger<CreateUserCommandHandler>> mockLogger,
            CreateUserCommand command,
            IMapper mapper,
            Mock<IConfiguration> mockConfiguration
        )
        {
            // Arrange
            var user = new Microsoft.Graph.Models.User
            {
                Id = id,
                DisplayName = displayName
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Microsoft.Graph.Models.User>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(user);

            //Act
            var sut = new CreateUserCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper,
                mockConfiguration.Object
            );
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Id.Should().Be(user.Id);
            result.DisplayName.Should().Be(user.DisplayName);

            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Microsoft.Graph.Models.User>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
