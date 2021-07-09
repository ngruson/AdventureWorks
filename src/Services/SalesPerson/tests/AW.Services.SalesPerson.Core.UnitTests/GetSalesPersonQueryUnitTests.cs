using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.Services.SalesPerson.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.Core.UnitTests
{
    public class GetSalesPersonQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesPersonExists_ReturnSalesPerson()
        {
            var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                })
                .CreateMapper();

            var loggerMock = new Mock<ILogger<GetSalesPersonQueryHandler>>();
            var salesPersonRepoMock = new Mock<IRepository<Entities.SalesPerson>>();

            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetSalesPersonSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(
                new TestBuilders.SalesPersonBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new GetSalesPersonQueryHandler(
                loggerMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonQuery
            {
                FirstName = "Stephen",
                MiddleName = "Y",
                LastName = "Jiang"
            };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetSalesPersonSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            result.FullName().Should().Be("Stephen Y Jiang");
        }

        [Fact]
        public void Handle_SalesPersonsNull_ThrowsArgumentNullException()
        {
            var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                })
                .CreateMapper();

            var loggerMock = new Mock<ILogger<GetSalesPersonQueryHandler>>();
            var salesPersonRepoMock = new Mock<IRepository<Entities.SalesPerson>>();

            var handler = new GetSalesPersonQueryHandler(
                loggerMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'salesPerson')");
        }
    }
}