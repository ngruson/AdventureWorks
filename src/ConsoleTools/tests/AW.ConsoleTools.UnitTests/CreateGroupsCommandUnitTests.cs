using AutoFixture.Xunit2;
using AW.ConsoleTools.Handlers.AzureAD.CreateGroups;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using Moq;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class CreateGroupsCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_GroupsDoNotExists_CreateGroups(
            string displayName,
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<CreateGroupsCommandHandler>> mockLogger,
            CreateGroupsCommand command
        )
        {
            // Arrange
            var response = new GroupCollectionResponse
            {
                Value = new List<Group>()
            };

            var group = new Group
            {
                DisplayName = displayName
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(response);

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Group>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(group);

            //Act
            var sut = new CreateGroupsCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
                )
            );

            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Group>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
                ),
                Times.Exactly(6)
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_SomeGroupsExists_CreateNewGroups(
            string displayName,
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<CreateGroupsCommandHandler>> mockLogger,
            CreateGroupsCommand command
        )
        {
            // Arrange

            var response = new GroupCollectionResponse
            {
                Value = new List<Group>
                {
                    new Group
                    {
                        DisplayName = "Executive General and Administration"
                    },
                    new Group
                    {
                        DisplayName = "Inventory Management"
                    },
                }
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(response);

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Group>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Group { DisplayName = displayName });

            //Act
            var sut = new CreateGroupsCommandHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object
            );
            await sut.Handle(command, CancellationToken.None);

            //Assert
            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
                )
            );

            mockRequestAdapter.Verify(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<Group>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
                ),
                Times.Exactly(4)
            );
        }
    }
}
