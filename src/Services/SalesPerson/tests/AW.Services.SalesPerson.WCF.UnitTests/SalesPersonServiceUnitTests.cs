using AutoFixture.Xunit2;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.Services.SalesPerson.WCF.Messages.GetSalesPerson;
using AW.Services.SalesPerson.WCF.Messages.ListSalesPersons;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesPerson.WCF.UnitTests
{
    public class SalesPersonServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task ListCustomers_ReturnsCustomers(
            [Frozen] Mock<IMediator> mockMediator,
            List<SalesPersonDto> salesPersons,
            SalesPersonService sut,
            ListSalesPersonsRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetSalesPersonsQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesPersons);

            //Act
            var result = await sut.ListSalesPersons(request);

            //Assert
            result.SalesPersons.Should().BeEquivalentTo(salesPersons);
        }

        [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.MappingProfile))]
        public async Task GetSalesPerson_ReturnsSalesPerson(
            [Frozen] Core.Handlers.GetSalesPerson.SalesPersonDto salesPerson,
            [Frozen] Mock<IMediator> mockMediator,            
            SalesPersonService sut,
            GetSalesPersonRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetSalesPersonQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesPerson);

            //Act
            var result = await sut.GetSalesPerson(request);

            //Assert
            result.SalesPerson.Should().BeEquivalentTo(salesPerson);
        }
    }
}