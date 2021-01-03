using AutoMapper;
using AW.Infrastructure.Api.WCF.AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Infrastructure.Api.WCF.UnitTests
{
    public class ContactTypeServiceAdapterUnitTests
    {
        [Fact]
        public async void ListContactypes_ReturnsContactTypes()
        {
            //Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ContactTypeProfile>())
                .CreateMapper();
            var mockLogger = new Mock<ILogger<ContactTypeServiceAdapter>>();
            var mockContactTypeService = new Mock<ContactTypeService.IContactTypeService>();
            mockContactTypeService.Setup(x => x.ListContactTypesAsync())
                .ReturnsAsync(new ContactTypeService.ListContactTypesResponse
                {
                    ContactTypes = new string[] { "Owner", "Order Administrator" }
                });

            var sut = new ContactTypeServiceAdapter(
                mockLogger.Object,
                mapper,
                mockContactTypeService.Object
            );

            //Act
            var response = await sut.ListContactTypesAsync();

            //Assert
            mockContactTypeService.Verify(x => x.ListContactTypesAsync());
            response.ContactTypes[0].Should().Be("Owner");
            response.ContactTypes[1].Should().Be("Order Administrator");
        }
    }
}