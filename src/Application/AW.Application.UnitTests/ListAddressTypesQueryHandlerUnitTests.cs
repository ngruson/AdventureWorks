using Ardalis.Specification;
using AW.Application.AddressType.ListAddressTypes;
using AW.Application.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Application.UnitTests
{
    public class ListAddressTypesQueryHandlerUnitTests
    {
        [Fact]
        public async void ListAddressTypes_AddressTypesExist_ReturnAddressTypes()
        {
            // Arrange
            var addressTypes = new List<Domain.Person.AddressType> {
                new Domain.Person.AddressType {  Name = "Home"},
                new Domain.Person.AddressType {  Name = "Main Office"},
                new Domain.Person.AddressType {  Name = "Main Primary"}
            };

            var repoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(addressTypes);

            var handler = new ListAddressTypesQueryHandler(repoMock.Object);

            //Act
            var result = await handler.Handle(new ListAddressTypesQuery(), CancellationToken.None);

            //Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public void ListAddressTypes_NoAddressTypesExist_ThrowException()
        {
            // Arrange
            var addressTypes = new List<Domain.Person.AddressType>();

            var repoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(addressTypes);

            var handler = new ListAddressTypesQueryHandler(repoMock.Object);

            //Act
            Func<Task> func = async() => await handler.Handle(new ListAddressTypesQuery(), CancellationToken.None);

            //Assert
            func.Should().Throw<AddressTypesNotFoundException>();
        }
    }
}