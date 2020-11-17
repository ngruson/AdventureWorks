using Ardalis.Specification;
using AW.Application.ContactType.ListContactTypes;
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
    public class ListContactTypesQueryHandlerUnitTests
    {
        [Fact]
        public async void ListContactTypes_ContactTypesExist_ReturnContactTypes()
        {
            // Arrange
            var contactTypes = new List<Domain.Person.ContactType> {
                new Domain.Person.ContactType {  Name = "Accounting Manager"},
                new Domain.Person.ContactType {  Name = "Owner"},
                new Domain.Person.ContactType {  Name = "Product Manager"}
            };

            var repoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(contactTypes);

            var handler = new ListContactTypesQueryHandler(repoMock.Object);

            //Act
            var result = await handler.Handle(new ListContactTypesQuery(), CancellationToken.None);

            //Assert
            result.Count().Should().Be(3);
        }

        [Fact]
        public void ListContactTypes_NoContactTypesExist_ThrowException()
        {
            // Arrange
            var contactTypes = new List<Domain.Person.ContactType>();

            var repoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            repoMock.Setup(x => x.ListAsync())
                .ReturnsAsync(contactTypes);

            var handler = new ListContactTypesQueryHandler(repoMock.Object);

            //Act
            Func<Task> func = async() => await handler.Handle(new ListContactTypesQuery(), CancellationToken.None);

            //Assert
            func.Should().Throw<ContactTypesNotFoundException>();
        }
    }
}