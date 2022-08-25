using AutoMapper;
using AW.SharedKernel.UnitTesting.Graph;
using AW.SharedKernel.UnitTesting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.ConsoleTools.AutoMapper;
using FluentAssertions;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AW.ConsoleTools.UnitTests
{
    public class GetGroupQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_GroupExists_ReturnGroup(
            Mock<ILogger<GetGroupQueryHandler>> mockLogger,
            IMapper mapper,
            string groupName
        )
        {
            // Arrange
            var group = new Microsoft.Graph.Group
            {
                DisplayName = groupName
            };

            string requestUrl = $"https://graph.microsoft.com/v1.0/groups?$expand=members&$filter=displayName eq %27{groupName}%27";
            var mockHttpProvider = new MockHttpProvider();
            mockHttpProvider.Responses.Add("GET:" + requestUrl,
                new GraphServiceGroupsCollectionResponse
                {
                    Value = new GraphServiceGroupsCollectionPage { group }
                }
            );

            var client = new GraphServiceClient(
                new MockAuthenticationHelper(),
                mockHttpProvider
            );

            var sut = new GetGroupQueryHandler(
                mockLogger.Object,
                client,
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
            Mock<ILogger<GetGroupQueryHandler>> mockLogger,
            IMapper mapper,
            string groupName
        )
        {
            // Arrange
            string requestUrl = $"https://graph.microsoft.com/v1.0/groups?$expand=members&$filter=displayName eq %27{groupName}%27";
            var mockHttpProvider = new MockHttpProvider();
            mockHttpProvider.Responses.Add("GET:" + requestUrl,
                new GraphServiceGroupsCollectionResponse
                {
                    Value = new GraphServiceGroupsCollectionPage()
                }
            );

            var client = new GraphServiceClient(
                new MockAuthenticationHelper(),
                mockHttpProvider
            );

            var sut = new GetGroupQueryHandler(
                mockLogger.Object,
                client,
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