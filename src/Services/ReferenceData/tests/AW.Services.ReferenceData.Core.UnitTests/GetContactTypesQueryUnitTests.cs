using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetContactTypesQueryUnitTests
    {
        [Fact]
        public async void Handle_ContactTypesExists_ReturnContactTypes()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetContactTypesQueryHandler>>();
            var contactTypeRepoMock = new Mock<IRepository<Entities.ContactType>>();

            contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Entities.ContactType>
                {
                    new TestBuilders.ContactTypeBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.ContactTypeBuilder()
                        .Name("Order Administrator")
                        .Build()
                });

            var handler = new GetContactTypesQueryHandler(
                loggerMock.Object,
                contactTypeRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetContactTypesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            result[0].Name.Should().Be("Owner");
            result[1].Name.Should().Be("Order Administrator");
        }

        [Fact]
        public void Handle_NoContactTypesExists_ThrowException()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetContactTypesQueryHandler>>();
            var contactTypeRepoMock = new Mock<IRepository<Entities.ContactType>>();

            var handler = new GetContactTypesQueryHandler(
                loggerMock.Object,
                contactTypeRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetContactTypesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}