using Ardalis.Specification;
using AutoMapper;
using AW.Common.Extensions;
using AW.Services.SalesPerson.Application.GetSalesPerson;
using AW.Services.SalesPerson.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.Application.UnitTests
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
            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.SalesPerson>>();

            salesPersonRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()))
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
            salesPersonRepoMock.Verify(x => x.GetBySpecAsync(It.IsAny<GetSalesPersonSpecification>()));

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
            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.SalesPerson>>();

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