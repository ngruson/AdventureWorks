using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Handlers.StateProvince.GetStatesProvinces;
using AW.Services.SharedKernel.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Exceptions;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetStateProvincesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoFilter_ReturnAllStateProvinces(
            List<Entities.StateProvince> statesProvinces,
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(
                    It.IsAny<GetStatesProvincesSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(statesProvinces);

            query.CountryRegionCode = "";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync(
                    It.IsAny<GetStatesProvincesSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            );

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(statesProvinces[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_StateProvincesExists_ReturnStateProvincesForCountry(
            List<Entities.StateProvince> statesProvinces,
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(statesProvinces);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            stateProvinceRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(statesProvinces[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoStateProvincesExists_ThrowStatesProvincesNotFoundException(
            [Frozen] Mock<IRepository<Entities.StateProvince>> stateProvinceRepoMock,
            GetStatesProvincesQueryHandler sut,
            GetStatesProvincesQuery query
        )
        {
            //Arrange
            stateProvinceRepoMock.Setup(x => x.ListAsync(
                    It.IsAny<GetStatesProvincesSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new List<Entities.StateProvince>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<StatesProvincesNotFoundException>();
            stateProvinceRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetStatesProvincesSpecification>(),
                It.IsAny<CancellationToken>()
                )
            );
        }
    }
}