using AW.Services.SharedKernel.EFCore.UnitTests.TestData;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SharedKernel.EFCore.UnitTests
{
    public class MediatorExtensionsUnitTests
    {
        private static ItemsContext CreateContext(ILogger<ItemsContext> logger, DbContextOptions<AWContext> options, IMediator mediator)
            => new(logger, options, mediator);

        [Theory, AutoMoqData]
        public async Task DispatchDomainEventsAsync_PublishDomainEvents(
            Mock<ILogger<ItemsContext>> mockLogger,
            Mock<IMediator> mockMediator,
            Mock<INotification> mockDomainEvent
        )
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(databaseName: "DispatchDomainEventsAsync_PublishDomainEvents")
                .Options;

            using var context = CreateContext(
                mockLogger.Object,
                options,
                mockMediator.Object
            );

            //Act
            var item = new Item { Name = "Item1" };
            item.AddDomainEvent(mockDomainEvent.Object);
            item.AddDomainEvent(mockDomainEvent.Object);
            context.Items.Add(item);

            await context.SaveChangesAsync();
            await mockMediator.Object.DispatchDomainEventsAsync(context);

            //Assert
            item.DomainEvents.Count.Should().Be(0);
            mockMediator.Verify(_ => _.Publish(
                It.IsAny<INotification>(),
                It.IsAny<CancellationToken>()
            ), Times.Exactly(2));
        }
    }
}