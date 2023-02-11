using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.GetSalesPersons;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests
{
    public class GetSalesPersonsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesPersonsExists_ReturnSalesPersons(
            List<Core.Entities.SalesPerson> salesPersons,
            [Frozen] Mock<IRepository<Core.Entities.SalesPerson>> salesPersonRepoMock,
            GetSalesPersonsQueryHandler sut
        )
        {
            //Arrange
            salesPersonRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesPersons);

            GetSalesPersonsQuery query = new(null);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesPersonRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result?.Count; i++)
            {
                result![i].Name!.FullName.Should().Be(salesPersons[i].Name!.FullName);
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

            for (int i = 0; i < result?.Count; i++)
            {
                result![i].Name!.FullName.Should().Be(salesPersons[i].Name!.FullName);
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            salesPersonRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetSalesPersonsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Core.Entities.SalesPerson>)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'salesPersons')");
        }
    }
}