using AW.Services.ReferenceData.Application.ContactType.GetContactTypes;
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
    public class ContactTypeServiceUnitTests
    {
        [Fact]
        public async Task ListContactTypes_ReturnsContactTypes()
        {
            //Arrange
            var contactTypes = new List<ContactType>
            {
                new ContactType { Name = "Owner" },
                new ContactType { Name = "Order Administrator" }
            };

            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetContactTypesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(contactTypes);

            var contactTypeService = new ContactTypeService(
                mockMediator.Object
            );

            //Act
            var result = await contactTypeService.ListContactTypes();

            //Assert
            result.Should().NotBeNull();
            result.ContactTypes.Count().Should().Be(2);
        }
    }
}