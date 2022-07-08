using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Internal.Controllers;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Internal.UnitTests.Controllers
{
    public class SalesTerritoryControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                List<Territory> territories
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetTerritoriesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(territories);

                var sut = new SalesTerritoryController(
                    mockMediator.Object
                );

                //Act
                var actionResult = await sut.Index();

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().BeEquivalentTo(territories);

                mockMediator.Verify(_ => _.Send(
                        It.IsAny<GetTerritoriesQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }
    }
}