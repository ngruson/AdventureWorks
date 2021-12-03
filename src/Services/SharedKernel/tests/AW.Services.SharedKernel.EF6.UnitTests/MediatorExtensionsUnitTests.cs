using AW.Services.SharedKernel.EF6.UnitTests.TestData;
using AW.Services.SharedKernel.EFCore.UnitTests.TestData;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SharedKernel.EF6.UnitTests
{
    public class MediatorExtensionsUnitTests
    {
        [Theory, AutoMoqData]
        public async Task DispatchDomainEventsAsync_PublishDomainEvents(
            Mock<IMediator> mockMediator,
            Mock<INotification> mockDomainEvent
        )
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ItemsContext(
                connection,
                true,
                null
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