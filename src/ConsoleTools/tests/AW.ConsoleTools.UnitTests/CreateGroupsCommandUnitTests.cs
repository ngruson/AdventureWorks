using AutoFixture.Xunit2;
using AW.ConsoleTools.Handlers.AzureAD.CreateGroups;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class CreateGroupsCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_GroupsDoNotExists_CreateGroups(
            GraphServiceGroupsCollectionPage page,
            Mock<Group> mockGroup,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<CreateGroupsCommandHandler>> mockLogger,
            CreateGroupsCommand command
        )
        {
            // Arrange
            mockGraphServiceClient.Setup(_ => _.Groups
                .Request()
                .OrderBy("displayName")
                .GetAsync(It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(() => page);

            mockGraphServiceClient.Setup(_ => _.Groups
                .Request()
                .AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(mockGroup.Object);

            //Act
            var sut = new CreateGroupsCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(Unit.Value);

            mockGraphServiceClient.Verify(_ => _.Groups
                .Request()
                .OrderBy("displayName")
                .GetAsync(It.IsAny<CancellationToken>())
            );
            mockGraphServiceClient.Verify(_ => _.Groups
                .Request()
                .AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>()),
                Times.Exactly(6)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_SomeGroupsExists_CreateNewGroups(
            GraphServiceGroupsCollectionPage page,
            Mock<Group> mockGroup,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<CreateGroupsCommandHandler>> mockLogger,
            CreateGroupsCommand command
        )
        {
            // Arrange
            page.Add(new Group
            {
                DisplayName = "Executive General and Administration"
            });
            page.Add(new Group
            {
                DisplayName = "Inventory Management"
            });
            
            mockGraphServiceClient.Setup(_ => _.Groups
                .Request()
                .OrderBy("displayName")
                .GetAsync(It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(() => page);

            mockGraphServiceClient.Setup(_ => _.Groups
                .Request()
                .AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(mockGroup.Object);

            //Act
            var sut = new CreateGroupsCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(Unit.Value);

            mockGraphServiceClient.Verify(_ => _.Groups
                .Request()
                .OrderBy("displayName")
                .GetAsync(It.IsAny<CancellationToken>())
            );
            mockGraphServiceClient.Verify(_ => _.Groups
                .Request()
                .AddAsync(It.IsAny<Group>(), It.IsAny<CancellationToken>()),
                Times.Exactly(4)
            );
        }
    }
}