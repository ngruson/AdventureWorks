using AutoFixture.Xunit2;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.EventBus.EFCore;
using AW.SharedKernel.EventBus.IntegrationEventLog;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SharedKernel.EFCore.UnitTests
{
    public class IntegrationEventLogServiceUnitTests
    {
        public class RetrieveEventLogsPendingToPublishAsync
        {
            [Theory, AutoMoqData]
            public async Task RetrieveEventLogsPendingToPublishAsync_EventLogsFound_ReturnOrderedEventLogs(
                [Frozen] Mock<AWContext> mockContext
            )
            {
                //Arrange
                var entries = new List<IntegrationEventLogEntry> {
                new IntegrationEventLogEntry(
                    new TestIntegrationEvent(),
                    Guid.NewGuid()
                )
            };

                var mockSet = entries.AsQueryable().BuildMockDbSet();

                mockContext.Setup(x => x.Set<IntegrationEventLogEntry>())
                    .Returns(mockSet.Object);

                var sut = new IntegrationEventLogService(
                    mockContext.Object,
                    typeof(IntegrationEventLogServiceUnitTests).Assembly
                );

                //Act
                var guid = Guid.Parse(entries[0].TransactionId);
                var eventLogs = await sut.RetrieveEventLogsPendingToPublishAsync(guid);

                //Assert
                eventLogs.Count().Should().Be(1);
            }

            [Theory, AutoMoqData]
            public async Task RetrieveEventLogsPendingToPublishAsync_NoEventLogsFound_ReturnEmptyList(
                [Frozen] Mock<AWContext> mockContext
            )
            {
                //Arrange
                var entries = new List<IntegrationEventLogEntry>();
                var mockSet = entries.AsQueryable().BuildMockDbSet();

                mockContext.Setup(x => x.Set<IntegrationEventLogEntry>())
                    .Returns(mockSet.Object);

                var sut = new IntegrationEventLogService(
                    mockContext.Object,
                    typeof(IntegrationEventLogServiceUnitTests).Assembly
                );

                //Act
                var guid = Guid.NewGuid();
                var eventLogs = await sut.RetrieveEventLogsPendingToPublishAsync(guid);

                //Assert
                eventLogs.Count().Should().Be(0);
            }
        }
    }
}