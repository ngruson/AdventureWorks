using AW.ConsoleTools.Handlers.AzureAD.AddUserToGroup;
using AW.ConsoleTools.Handlers.AzureAD.GetGroup;
using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.UnitTesting.Graph;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class AddUserToGroupCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_UserExists_AddUserToGroup(
            Mock<ILogger<AddUserToGroupCommandHandler>> mockLogger,
            AddUserToGroupCommand command
        )
        {
            // Arrange
            string requestUrl = $"https://graph.microsoft.com/v1.0/users?$expand=memberOf&$filter=displayName eq %27{command.UserName}%27";
            var mockHttpProvider = new MockHttpProvider();

            mockHttpProvider.Responses.Add("GET:" + requestUrl,
                new GraphServiceUsersCollectionResponse
                {
                    Value = new GraphServiceUsersCollectionPage 
                    { 
                        new User { 
                            Id = Guid.NewGuid().ToString(),
                            DisplayName = command.UserName 
                        }
                    }
                }
            );

            var client = new GraphServiceClient(
                new MockAuthenticationHelper(),
                mockHttpProvider
            );

            //Act
            var sut = new AddUserToGroupCommandHandler(
                mockLogger.Object,
                client
            );
            var result = await sut.Handle(
                command,
                CancellationToken.None
            );

            //Assert
            result.Should().Be(Unit.Value);
        }

        [Theory, AutoMoqData]
        public async Task Handle_UserDoesNotExist_ThrowArgumentNullException(
            Mock<ILogger<AddUserToGroupCommandHandler>> mockLogger,
            AddUserToGroupCommand command
        )
        {
            // Arrange
            string requestUrl = $"https://graph.microsoft.com/v1.0/users?$expand=memberOf&$filter=displayName eq %27{command.UserName}%27";
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

            //Act
            var sut = new AddUserToGroupCommandHandler(
                mockLogger.Object,
                client
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