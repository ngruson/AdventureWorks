using AutoFixture.Xunit2;
using AutoMapper;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.Handlers.AzureAD.GetUser;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Serialization;
using Moq;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class GetUserQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserExists_ReturnUser(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<GetUserQueryHandler>> mockLogger,
            IMapper mapper,
            string userName,
            string groupId,
            string groupName
        )
        {
            // Arrange
            var user = new Microsoft.Graph.Models.User
            {
                DisplayName = userName,
                MemberOf = new List<DirectoryObject>
                {
                    
                    new Microsoft.Graph.Models.Group
                    {
                        Id = groupId,
                        DisplayName = groupName
                    }
                }
            };

            var users = new UserCollectionResponse
            {
                Value = new List<Microsoft.Graph.Models.User> { user }
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<UserCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(users);

            var sut = new GetUserQueryHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper
            );

            //Act
            var result = await sut.Handle(
                new GetUserQuery(userName),
                CancellationToken.None
            );

            //Assert
            result!.DisplayName.Should().BeEquivalentTo(user.DisplayName);
            var resultGroup = user.MemberOf[0] as Microsoft.Graph.Models.Group;
            result.MemberOf!.Count.Should().Be(user.MemberOf.Count);
            result.MemberOf[0].Id.Should().BeEquivalentTo(resultGroup!.Id);
            result.MemberOf[0].DisplayName.Should().BeEquivalentTo(resultGroup.DisplayName);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserNotFound_ReturnNull(
            [Frozen] Mock<IRequestAdapter> mockRequestAdapter,
            [Frozen] Mock<GraphServiceClient> mockGraphServiceClient,
            Mock<ILogger<GetUserQueryHandler>> mockLogger,
            IMapper mapper,
            string userName
        )
        {
            // Arrange
            var users = new UserCollectionResponse
            {
                Value = new List<Microsoft.Graph.Models.User>()
            };

            mockRequestAdapter.Setup(_ => _.SendAsync(
                It.IsAny<RequestInformation>(),
                It.IsAny<ParsableFactory<UserCollectionResponse>>(),
                It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(users);

            var sut = new GetUserQueryHandler(
                mockLogger.Object,
                mockGraphServiceClient.Object,
                mapper
            );

            //Act
            var result = await sut.Handle(
                new GetUserQuery(userName),
                CancellationToken.None
            );

            //Assert
            result!.Should().BeNull();
        }
    }
}
