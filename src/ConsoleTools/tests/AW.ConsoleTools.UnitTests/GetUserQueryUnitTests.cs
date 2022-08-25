using AutoMapper;
using AW.ConsoleTools.AutoMapper;
using AW.ConsoleTools.Handlers.AzureAD.GetUser;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.UnitTesting.Graph;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class GetUserQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserExists_ReturnUser(
            Mock<ILogger<GetUserQueryHandler>> mockLogger,
            IMapper mapper,
            string userName,
            string groupId,
            string groupName
        )
        {
            // Arrange
            var user = new Microsoft.Graph.User
            {
                DisplayName = userName,
                MemberOf = new UserMemberOfCollectionWithReferencesPage
                {
                    
                    new Microsoft.Graph.Group
                    {
                        Id = groupId,
                        DisplayName = groupName
                    }
                }
            };

            string requestUrl = $"https://graph.microsoft.com/v1.0/users?$expand=memberOf&$filter=displayName eq %27{userName}%27";
            var mockHttpProvider = new MockHttpProvider();
            mockHttpProvider.Responses.Add("GET:" + requestUrl,
                new GraphServiceUsersCollectionResponse
                {
                    Value = new GraphServiceUsersCollectionPage { user }
                }
            );

            var client = new GraphServiceClient(
                new MockAuthenticationHelper(), 
                mockHttpProvider
            );

            var sut = new GetUserQueryHandler(
                mockLogger.Object,
                client,
                mapper
            );

            //Act
            var result = await sut.Handle(
                new GetUserQuery(userName),
                CancellationToken.None
            );

            //Assert
            result?.DisplayName.Should().BeEquivalentTo(user.DisplayName);
            var resultGroup = user.MemberOf[0] as Microsoft.Graph.Group;
            result?.MemberOf!.Count.Should().Be(user.MemberOf.Count);
            result?.MemberOf![0].Id.Should().BeEquivalentTo(resultGroup!.Id);
            result?.MemberOf![0].DisplayName.Should().BeEquivalentTo(resultGroup!.DisplayName);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UserNotFound_ReturnNull(
            Mock<ILogger<GetUserQueryHandler>> mockLogger,
            IMapper mapper,
            string userName
        )
        {
            // Arrange
            string requestUrl = $"https://graph.microsoft.com/v1.0/users?$expand=memberOf&$filter=displayName eq %27{userName}%27";
            var mockHttpProvider = new MockHttpProvider();
            mockHttpProvider.Responses.Add("GET:" + requestUrl,
                new GraphServiceUsersCollectionResponse
                {
                    Value = new GraphServiceUsersCollectionPage()
                }
            );

            var client = new GraphServiceClient(
                new MockAuthenticationHelper(),
                mockHttpProvider
            );

            var sut = new GetUserQueryHandler(
                mockLogger.Object,
                client,
                mapper
            );

            //Act
            var result = await sut.Handle(
                new GetUserQuery(userName),
                CancellationToken.None
            );

            //Assert
            result?.Should().BeNull();
        }
    }
}