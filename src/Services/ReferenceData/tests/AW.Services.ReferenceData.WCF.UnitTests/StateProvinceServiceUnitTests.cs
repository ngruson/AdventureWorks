using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.ReferenceData.WCF.Messages.ListStatesProvinces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.WCF.UnitTests
{
    public class StateProvinceServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListStateProvinces_ReturnsStateProvinces(
            List<StateProvince> statesProvinces,
            [Frozen] Mock<IMediator> mockMediator,
            StateProvinceService sut,
            ListStatesProvincesRequest request
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetStatesProvincesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(statesProvinces);

            //Act
            var result = await sut.ListStatesProvinces(request);

            //Assert
            result.Should().NotBeNull();
            result.StateProvinces.Count().Should().Be(statesProvinces.Count);
        }
    }
}