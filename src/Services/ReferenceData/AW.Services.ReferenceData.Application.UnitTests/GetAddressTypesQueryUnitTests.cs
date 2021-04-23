using Ardalis.Specification;
using AW.Services.ReferenceData.Application.AddressType.GetAddressTypes;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Application.UnitTests
{
    public class GetAddressTypesQueryUnitTests
    {
        [Fact]
        public async void Handle_AddressTypesExists_ReturnAddressTypes()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetAddressTypesQueryHandler>>();
            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.AddressType>>();

            addressTypeRepoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(new List<Domain.AddressType>
                {
                    new TestBuilders.AddressTypeBuilder()
                        .WithTestValues()
                        .Build(),

                    new TestBuilders.AddressTypeBuilder()
                        .Name("Main Office")
                        .Build()
                });

            var handler = new GetAddressTypesQueryHandler(
                loggerMock.Object,
                addressTypeRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetAddressTypesQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            addressTypeRepoMock.Verify(x => x.ListAsync());
            result[0].Name.Should().Be("Home");
            result[1].Name.Should().Be("Main Office");
        }

        [Fact]
        public void Handle_NoAddressTypesExists_ThrowException()
        {
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<GetAddressTypesQueryHandler>>();
            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.AddressType>>();

            var handler = new GetAddressTypesQueryHandler(
                loggerMock.Object,
                addressTypeRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetAddressTypesQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            addressTypeRepoMock.Verify(x => x.ListAsync());
        }
    }
}