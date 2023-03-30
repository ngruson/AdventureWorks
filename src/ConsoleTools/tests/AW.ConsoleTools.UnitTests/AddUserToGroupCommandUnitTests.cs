using AutoFixture.Xunit2;
using AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using Xunit;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;

namespace AW.ConsoleTools.UnitTests
{
    public class AddUserToGroupCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_UserExists_AddUserToGroup(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<AddUserToGroupCommandHandler>> mockLogger,
            AddUserToGroupCommand command
        )
        {
            // Arrange
            var user = new User
            {
                DisplayName = command.UserName
            };

            var users = new UserCollectionResponse
            {
                Value = new List<User> { user }
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<UserCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(users);

            //Act
            var sut = new AddUserToGroupCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );

            await sut.Handle(
                command,
                CancellationToken.None
            );

            //Assert
            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<UserCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task Handle_UserDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<AddUserToGroupCommandHandler>> mockLogger,
            AddUserToGroupCommand command
        )
        {
            // Arrange
            var users = new UserCollectionResponse
            {
                Value = new List<User>()
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<UserCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(users);

            //Act
            var sut = new AddUserToGroupCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );

            Func<Task> func = async () => await sut.Handle(
                command,
                CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'user')");
        }
    }
}
