using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Exceptions;
using AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetShipMethodsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ShipMethodsExists_ReturnShipMethods(
            List<Entities.ShipMethod> shipMethods,
            [Frozen] Mock<IRepository<Entities.ShipMethod>> shipMethodRepoMock,
            GetShipMethodsQueryHandler sut,
            GetShipMethodsQuery query
        )
        {
            //Arrange
            shipMethodRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(shipMethods);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            shipMethodRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(shipMethods[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoShipMethodsExists_ThrowShipMethodsNotFoundException(
            [Frozen] Mock<IRepository<Entities.ShipMethod>> shipMethodRepoMock,
            GetShipMethodsQueryHandler sut,
            GetShipMethodsQuery query
        )
        {
            //Arrange
            shipMethodRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.ShipMethod>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ShipMethodsNotFoundException>();
            shipMethodRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}