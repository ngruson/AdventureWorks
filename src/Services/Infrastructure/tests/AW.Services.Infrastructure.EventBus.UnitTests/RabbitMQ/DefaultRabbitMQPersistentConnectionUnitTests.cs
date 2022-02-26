using AutoFixture.Xunit2;
using AW.Services.Infrastructure.EventBus.RabbitMQ;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using System;
using System.IO;
using Xunit;

namespace AW.Services.Infrastructure.EventBus.UnitTests.RabbitMQ
{
    public class DefaultRabbitMQPersistentConnectionUnitTests
    {
        public class IsConnected
        {
            [Theory, AutoMoqData]
            public void IsConnected_ConnectionIsOpen_ReturnsTrue(
                [Frozen] Mock<IConnectionFactory> mockConnectionFactory,
                [Frozen] Mock<IConnection> mockConnection,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.IsOpen)
                    .Returns(true);

                mockConnectionFactory.Setup(_ => _.CreateConnection())
                    .Returns(mockConnection.Object);

                //Act
                sut.TryConnect();
                var result = sut.IsConnected;

                //Assert
                result.Should().BeTrue();
            }

            [Theory, AutoMoqData]
            public void IsConnected_ConnectionIsNull_ReturnsFalse(
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange

                //Act
                var result = sut.IsConnected;

                //Assert
                result.Should().BeFalse();
            }

            [Theory, AutoMoqData]
            public void IsConnected_ConnectionIsDisposed_ReturnsFalse(
                [Frozen] Mock<IConnectionFactory> mockConnectionFactory,
                [Frozen] Mock<IConnection> mockConnection,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.IsOpen)
                    .Returns(true);

                mockConnectionFactory.Setup(_ => _.CreateConnection())
                    .Returns(mockConnection.Object);

                //Act
                sut.TryConnect();
                sut.Dispose();

                //Assert
                mockConnection.Verify(_ => _.Dispose());
            }
        }

        public class CreateModel
        {
            [Theory, AutoMoqData]
            public void CreateModel_ConnectionIsOpen_ReturnsModel(
                [Frozen] Mock<IConnectionFactory> mockConnectionFactory,
                [Frozen] Mock<IConnection> mockConnection,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.IsOpen)
                    .Returns(true);

                mockConnectionFactory.Setup(_ => _.CreateConnection())
                    .Returns(mockConnection.Object);

                //Act
                sut.TryConnect();
                var result = sut.CreateModel();

                //Assert
                result.Should().NotBeNull();
            }

            [Theory, AutoMoqData]
            public void CreateModel_ConnectionIsClosed_ThrowsInvalidOperationException(
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange

                //Act
                Action act = () => sut.CreateModel();

                //Assert
                act.Should().Throw<InvalidOperationException>();
            }
        }

        public class Dispose
        {
            [Theory, AutoMoqData]
            public void Dispose_ConnectionIsNotNull_NoLog(
                [Frozen] Mock<ILogger<DefaultRabbitMQPersistentConnection>> mockLogger,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange

                //Act
                sut.TryConnect();
                sut.Dispose();

                //Assert
                mockLogger.Verify(
                    _ => _.Log(
                        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                        It.Is<EventId>(eventId => eventId.Id == 0),
                        It.IsAny<It.IsAnyType>(),
                        It.IsAny<Exception>(),
                        It.IsAny<Func<It.IsAnyType, Exception, string>>()
                    ),
                    Times.Never
                );
            }

            [Theory, AutoMoqData]
            public void Dispose_ConnectionIsNull_NoLog(
                [Frozen] Mock<ILogger<DefaultRabbitMQPersistentConnection>> mockLogger,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange

                //Act
                sut.Dispose();

                //Assert
                mockLogger.Verify(
                    _ => _.Log(
                        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                        It.Is<EventId>(eventId => eventId.Id == 0),
                        It.IsAny<It.IsAnyType>(),
                        It.IsAny<Exception>(),
                        It.IsAny<Func<It.IsAnyType, Exception, string>>()
                    ),
                    Times.Never
                );
            }

            [Theory, AutoMoqData]
            public void Dispose_ConnectionDisposeThrowsIOException_LogCritical(
                [Frozen] Mock<ILogger<DefaultRabbitMQPersistentConnection>> mockLogger,
                [Frozen] Mock<IConnectionFactory> mockConnectionFactory,
                [Frozen] Mock<IConnection> mockConnection,
                DefaultRabbitMQPersistentConnection sut
            )
            {
                //Arrange
                mockConnection.Setup(_ => _.IsOpen)
                    .Returns(true);
                mockConnection.Setup(_ => _.Dispose())
                    .Throws<IOException>();

                mockConnectionFactory.Setup(_ => _.CreateConnection())
                    .Returns(mockConnection.Object);

                //Act
                sut.TryConnect();
                sut.Dispose();

                //Assert
                mockLogger.Verify(
                    _ => _.Log(
                        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Critical),
                        It.Is<EventId>(eventId => eventId.Id == 0),
                        It.IsAny<It.IsAnyType>(),
                        It.IsAny<Exception>(),
                        (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
                    ),
                    Times.Once
                );
            }
        }
    }
}