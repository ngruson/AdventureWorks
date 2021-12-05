using AW.Services.SharedKernel.EF6.UnitTests.TestData;
using AW.Services.SharedKernel.EFCore.UnitTests.TestData;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SharedKernel.EF6.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public async Task SetModified_EntityStateIsModified()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ItemsContext(
                connection,
                true,
                null
            );

            context.Items.Add(new Item { Name = "Item1" });
            await context.SaveChangesAsync();

            //Act
            var item = context.Items.Single(i => i.Name == "Item1");
            context.SetModified(item);

            //Assert
            context.Entry(item).State.Should().Be(EntityState.Modified);
        }

        [Fact]
        public async Task SaveEntitiesAsync_ChangesAreSaved_ReturnsTrue()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ItemsContext(
                connection,
                true,
                null
            );

            //Act
            context.Items.Add(new Item { Name = "Item1" });
            var result = await context.SaveEntitiesAsync();

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task Execute_FuncIsExecuted()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ItemsContext(
                connection,
                true,
                null
            );

            //Act
            int result = 0;
            Func<Task> func = () =>
            {
                result = 2 + 2;
                return Task.FromResult(result);
            };
            await context.Execute(func);

            //Assert
            result.Should().Be(4);
        }

        [Fact]
        public async Task BeginTransactionAsync_TransactionIsStarted()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new ItemsContext(
                connection,
                true,
                null
            );

            //Act
            var transaction = await context.BeginTransactionAsync();

            //Assert
            transaction.Should().Be(context.CurrentTransaction);
            context.CurrentTransactionId.Should().NotBeEmpty();
        }

        public class CommitTransactionAsync
        {
            [Fact]
            public async Task CommitTransactionAsync_TransactionIsCommittedAndDisposed()
            {
                //Arrange
                var connection = Effort.DbConnectionFactory.CreateTransient();
                var context = new ItemsContext(
                    connection,
                    true,
                    null
                );

                var transaction = await context.BeginTransactionAsync();

                //Act
                await context.CommitTransactionAsync(transaction);

                //Assert
                context.CurrentTransaction.Should().BeNull();
            }

            [Fact]
            public async Task CommitTransactionAsync_TransactionIsNull_ThrowArgumentNullException()
            {
                //Arrange
                var connection = Effort.DbConnectionFactory.CreateTransient();
                var context = new ItemsContext(
                    connection,
                    true,
                    null
                );

                //Act
                Func<Task> func = async() => await context.CommitTransactionAsync(null);

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>();
            }

            [Theory, AutoMoqData]
            public async Task CommitTransactionAsync_TransactionIsOtherThanExpected_ThrowInvalidOperationException(
                DbTransaction dbTransaction                
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
                Func<Task> func = async () => await context.CommitTransactionAsync(dbTransaction);

                //Assert
                await func.Should().ThrowAsync<InvalidOperationException>();
            }
        }

        public class RollbackTransaction
        {
            [Fact]
            public async Task RollbackTransaction_TransactionIsStarted_RollbackAndDispose()
            {
                //Arrange
                var connection = Effort.DbConnectionFactory.CreateTransient();
                var context = new ItemsContext(
                    connection,
                    true,
                    null
                );

                await context.BeginTransactionAsync();

                //Act
                context.RollbackTransaction();

                //Assert
                context.CurrentTransaction.Should().BeNull();
            }

            [Fact]
            public void RollbackTransaction_TransactionIsNull_Ignore()
            {
                //Arrange
                var connection = Effort.DbConnectionFactory.CreateTransient();
                var context = new ItemsContext(
                    connection,
                    true,
                    null
                );

                //Act
                context.RollbackTransaction();

                //Assert
                context.CurrentTransaction.Should().BeNull();
            }
        }
    }
}