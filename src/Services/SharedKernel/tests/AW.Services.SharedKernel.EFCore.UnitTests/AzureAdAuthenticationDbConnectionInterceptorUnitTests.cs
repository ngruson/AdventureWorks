using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using Azure.Core;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using Xunit;

namespace AW.Services.SharedKernel.EFCore.UnitTests
{
    public class AzureAdAuthenticationDbConnectionInterceptorUnitTests
    {
        public class ConnectionOpening
        {
            [Theory, AutoMoqData]
            public void DoNotSetAccessTokenGivenNonAzureSqlConnection(
                AzureAdAuthenticationDbConnectionInterceptor sut,
                Mock<EventDefinitionBase> mockEventDefinition,
                Func<EventDefinitionBase, EventData, string> messageGenerator,
                [Frozen] Mock<DbContext> mockContext,
                Guid connectionId,
                DateTimeOffset startTime,
                InterceptionResult interceptionResult
            )
            {
                //Arrange
                var connectionString = "Server=myServerAddress;Database=myDataBase";
                var connection = new SqlConnection(connectionString);

                var eventData = new ConnectionEventData(
                    mockEventDefinition.Object,
                    messageGenerator,
                    connection,
                    mockContext.Object,
                    connectionId,
                    false,
                    startTime
                );

                //Act
                var result = sut.ConnectionOpening(
                    connection,
                    eventData,
                    interceptionResult
                );

                //Assert
                result.Should().NotBeNull();
                connection.AccessToken.Should().BeNullOrEmpty();
            }

            [Theory, AutoMoqData]
            public void SetAccessTokenGivenAzureSqlConnection(
                AccessToken accessToken,
                Mock<TokenCredential> mockCredential,
                Mock<EventDefinitionBase> mockEventDefinition,
                Func<EventDefinitionBase, EventData, string> messageGenerator,
                [Frozen] Mock<DbContext> mockContext,
                Guid connectionId,
                DateTimeOffset startTime,
                InterceptionResult result
            )
            {
                //Arrange
                var connectionString = "Server=myServerAddress.database.windows.net;Database=myDataBase";
                var connection = new SqlConnection(connectionString);

                var eventData = new ConnectionEventData(
                    mockEventDefinition.Object,
                    messageGenerator,
                    connection,
                    mockContext.Object,
                    connectionId,
                    false,
                    startTime
                );

                mockCredential.Setup(_ => _.GetToken(
                    It.IsAny<TokenRequestContext>(),
                    It.IsAny<CancellationToken>()
                    )
                )
                .Returns(accessToken);

                var sut = new AzureAdAuthenticationDbConnectionInterceptor(
                    mockCredential.Object
                );

                //Act
                sut.ConnectionOpening(
                    connection,
                    eventData,
                    result
                );

                //Assert
                connection.AccessToken.Should().Be(accessToken.Token);
            }
        }

        public class ConnectionOpeningAsync
        {
            [Theory, AutoMoqData]
            public async Task DoNotSetAccessTokenGivenNonAzureSqlConnection(
                AzureAdAuthenticationDbConnectionInterceptor sut,
                Mock<EventDefinitionBase> mockEventDefinition,
                Func<EventDefinitionBase, EventData, string> messageGenerator,
                [Frozen] Mock<DbContext> mockContext,
                Guid connectionId,
                DateTimeOffset startTime,
                InterceptionResult interceptionResult
            )
            {
                //Arrange
                var connectionString = "Server=myServerAddress;Database=myDataBase";
                var connection = new SqlConnection(connectionString);

                var eventData = new ConnectionEventData(
                    mockEventDefinition.Object,
                    messageGenerator,
                    connection,
                    mockContext.Object,
                    connectionId,
                    false,
                    startTime
                );

                //Act
                await sut.ConnectionOpeningAsync(
                    connection,
                    eventData,
                    interceptionResult
                );

                //Assert
                connection.AccessToken.Should().BeNullOrEmpty();
            }

            [Theory, AutoMoqData]
            public async Task SetAccessTokenGivenAzureSqlConnection(
                //AccessToken accessToken,
                //Mock<TokenCredential> mockCredential,
                Mock<EventDefinitionBase> mockEventDefinition,
                Func<EventDefinitionBase, EventData, string> messageGenerator,
                [Frozen] Mock<DbContext> mockContext,
                Guid connectionId,
                DateTimeOffset startTime,
                InterceptionResult result
            )
            {
                //Arrange
                var connectionString = "Server=myServerAddress.database.windows.net;Database=myDataBase";
                var connection = new SqlConnection(connectionString);

                var eventData = new ConnectionEventData(
                    mockEventDefinition.Object,
                    messageGenerator,
                    connection,
                    mockContext.Object,
                    connectionId,
                    false,
                    startTime
                );

                //mockCredential.Setup(_ => _.GetToken(
                //    It.IsAny<TokenRequestContext>(),
                //    It.IsAny<CancellationToken>()
                //    )
                //)
                //.Returns(accessToken);

                var sut = new AzureAdAuthenticationDbConnectionInterceptor(
                    //mockCredential.Object
                );

                //Act
                await sut.ConnectionOpeningAsync(
                    connection,
                    eventData,
                    result
                );

                //Assert
                connection.AccessToken.Should().NotBeEmpty();
            }
        }
    }
}
