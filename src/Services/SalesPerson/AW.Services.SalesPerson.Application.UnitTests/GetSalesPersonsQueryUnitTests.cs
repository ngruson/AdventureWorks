using Ardalis.Specification;
using AutoMapper;
using AW.Common.Extensions;
using AW.Services.SalesPerson.Application.GetSalesPersons;
using AW.Services.SalesPerson.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.Application.UnitTests
{
    public class GetSalesPersonsQueryUnitTests
    {
        [Fact]
        public async void Handle_SalesPersonsExists_ReturnSalesPersons()
        {
            var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                })
                .CreateMapper();

            var loggerMock = new Mock<ILogger<GetSalesPersonsQueryHandler>>();
            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.SalesPerson>>();

            salesPersonRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesPersonsSpecification>()))
                .ReturnsAsync(new List<Domain.SalesPerson>
                {
                    new TestBuilders.SalesPersonBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesPersonBuilder()
                        .FirstName("Michael")
                        .MiddleName("G")
                        .LastName("Blythe")
                        .Build(),
                });

            var handler = new GetSalesPersonsQueryHandler(
                loggerMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonsQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.ListAsync(It.IsAny<GetSalesPersonsSpecification>()));

            result[0].FullName().Should().Be("Stephen Y Jiang");
            result[1].FullName().Should().Be("Michael G Blythe");
        }

        [Fact]
        public async void Handle_TerritoryFilter_ReturnSalesPersons()
        {
            var mapper = new MapperConfiguration(opts =>
            {
                opts.AddProfile<MappingProfile>();
            })
                .CreateMapper();

            var loggerMock = new Mock<ILogger<GetSalesPersonsQueryHandler>>();
            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.SalesPerson>>();

            salesPersonRepoMock.Setup(x => x.ListAsync(It.IsAny<GetSalesPersonsSpecification>()))
                .ReturnsAsync(new List<Domain.SalesPerson>
                {
                    new TestBuilders.SalesPersonBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.SalesPersonBuilder()
                        .FirstName("Michael")
                        .MiddleName("G")
                        .LastName("Blythe")
                        .Build(),
                });

            var handler = new GetSalesPersonsQueryHandler(
                loggerMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonsQuery
            {
                Territory = "Northeast"
            };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.ListAsync(It.IsAny<GetSalesPersonsSpecification>()));

            result[0].FullName().Should().Be("Stephen Y Jiang");
            result[1].FullName().Should().Be("Michael G Blythe");
        }

        [Fact]
        public void Handle_SalesPersonsNull_ThrowsArgumentNullException()
        {
            var mapper = new MapperConfiguration(opts =>
                {
                    opts.AddProfile<MappingProfile>();
                })
                .CreateMapper();

            var loggerMock = new Mock<ILogger<GetSalesPersonsQueryHandler>>();
            var salesPersonRepoMock = new Mock<IRepositoryBase<Domain.SalesPerson>>();

            var handler = new GetSalesPersonsQueryHandler(
                loggerMock.Object,
                salesPersonRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetSalesPersonsQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'salesPersons')");
        }
    }
}