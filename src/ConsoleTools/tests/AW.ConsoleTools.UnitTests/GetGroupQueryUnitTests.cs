using AutoMapper;
using AW.SharedKernel.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using Xunit;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.ConsoleTools.AutoMapper;
using FluentAssertions;
using AutoFixture.Xunit2;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;

namespace AW.ConsoleTools.UnitTests
{
    public class GetGroupQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_GroupExists_ReturnGroup(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<GetGroupQueryHandler>> mockLogger,
            IMapper mapper,
            string groupName
        )
        {
            // Arrange
            var group = new Microsoft.Graph.Models.Group
            {
                DisplayName = groupName
            };

            var groups = new GroupCollectionResponse
            {
                Value = new List<Microsoft.Graph.Models.Group> { group }
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(groups);

            var sut = new GetGroupQueryHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper
            );

            //Act
            var result = await sut.Handle(
                new GetGroupQuery(groupName),
                CancellationToken.None
            );

            //Assert
            result?.DisplayName?.Should().BeEquivalentTo(group.DisplayName);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_GroupNotFound_ThrowsArgumentNullException(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<GetGroupQueryHandler>> mockLogger,
            IMapper mapper,
            string groupName
        )
        {
            // Arrange            
            var groups = new GroupCollectionResponse
            {
                Value = new List<Microsoft.Graph.Models.Group>()
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<GroupCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(groups);

            var sut = new GetGroupQueryHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper
            );

            //Act
            Func<Task> func = async () => await sut.Handle(
                new GetGroupQuery(groupName), 
                CancellationToken.None
            );

            //Assert
            await func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'group')");
        }
    }
}
