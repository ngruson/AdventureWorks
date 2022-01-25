using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Handlers.GetSalesPersons;
using AW.Services.Sales.Core.Specifications;
using AW.SharedKernel.Extensions;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests
{
    public class GetSalesPersonsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesPersonsExists_ReturnSalesPersons(
            List<Core.Entities.SalesPerson> salesPersons,
            [Frozen] Mock<IRepository<Core.Entities.SalesPerson>> salesPersonRepoMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            salesPersonRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesPersons);

            query.Territory = "";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].FullName().Should().Be(salesPersons[i].FullName);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_TerritoryFilter_ReturnSalesPersons(
            List<Core.Entities.SalesPerson> salesPersons,
            [Frozen] Mock<IRepository<Core.Entities.SalesPerson>> salesPersonRepoMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            salesPersonRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesPersons);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].FullName().Should().Be(salesPersons[i].FullName);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_SalesPersonsNull_ThrowsArgumentNullException(
            [Frozen] Mock<IRepository<Core.Entities.SalesPerson>> salesPersonRepoMock,
            GetSalesPersonsQueryHandler sut,
            GetSalesPersonsQuery query
        )
        {
            //Arrange
            salesPersonRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Core.Entities.SalesPerson>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'salesPersons')");
        }
    }
}