using AW.Services.SharedKernel.EFCore.UnitTests.TestData;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Services.SharedKernel.EFCore.UnitTests
{
    public class AWContextUnitTests
    {
        private static ItemsContext CreateContext(ILogger<ItemsContext> logger, DbContextOptions<AWContext> options, IMediator mediator)
            => new(logger, options, mediator);

        [Theory, AutoMoqData]
        public async Task SetModified_EntityStateIsModified(
            Mock<ILogger<ItemsContext>> mockLogger,
            Mock<IMediator> mockMediator,
            Item item
        )
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(databaseName: nameof(SetModified_EntityStateIsModified))
                .Options;

            using var context = CreateContext(
                mockLogger.Object, 
                options, 
                mockMediator.Object
            );

            context.Items.Add(item);
            await context.SaveChangesAsync();

            //Act
            context.SetModified(item);

            //Assert
            context.Entry(item).State.Should().Be(EntityState.Modified);
        }

        [Theory, AutoMoqData]
        public async Task SaveEntitiesAsync_ChangesAreSaved_DomainEventsAreDispatched(
            Mock<ILogger<ItemsContext>> mockLogger,
            Mock<IMediator> mockMediator,
            Item item
        )
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(databaseName: nameof(SaveEntitiesAsync_ChangesAreSaved_DomainEventsAreDispatched))
                .Options;

            using var context = CreateContext(
                mockLogger.Object,
                options,
                mockMediator.Object
            );

            //Act
            context.Items.Add(item);
            var result = await context.SaveEntitiesAsync();

            //Assert
            result.Should().Be(true);
        }

        [Theory, AutoMoqData]
        public async Task Execute_FuncIsExecuted(
            Mock<ILogger<ItemsContext>> mockLogger,
            Mock<IMediator> mockMediator
        )
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(databaseName: nameof(Execute_FuncIsExecuted))
                .Options;

            using var context = CreateContext(
                mockLogger.Object,
                options,
                mockMediator.Object
            );

            //Act
            int result = 0;
            Task func()
            {
                result = 2 + 2;
                return Task.FromResult(result);
            }
            await context.Execute(func);

            //Assert
            result.Should().Be(4);
        }

        [Theory, AutoMoqData]
        public async Task BeginTransactionAsync_TransactionIsStarted(
            Mock<ILogger<ItemsContext>> mockLogger,
            Mock<IMediator> mockMediator
        )
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(databaseName: nameof(BeginTransactionAsync_TransactionIsStarted))
                .ConfigureWarnings(config => config.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using var context = CreateContext(
                mockLogger.Object,
                options,
                mockMediator.Object
            );

            //Act
            Func<Task> func = async() => await context.BeginTransactionAsync();

            //Assert
            await func.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Relational-specific methods can only be used when the context is using a relational database provider.");
        }
    }
}