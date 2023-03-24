using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Infrastructure.EventBus.Events;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class CreateSalesOrderCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnsTrueGivenCreditCardExists(
            [Frozen] Mock<ISalesOrderIntegrationEventService> salesOrderIntegrationEventServiceMock,
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            [Frozen] Mock<IRepository<CreditCard>> creditCardRepositoryMock,
            [Frozen] Mock<IRepository<SpecialOfferProduct>> specialOfferProductRepositoryMock,
            List<SpecialOfferProduct> specialOffers,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command,
            CreditCard creditCard,
            DateTime expirationDate
        )
        {
            //Arrange
            command.OrderItems?.ForEach(_ => _.UnitPriceDiscount = 0);

            creditCard.ExpMonth = byte.Parse(expirationDate.Month.ToString());
            creditCard.ExpYear = short.Parse(expirationDate.Year.ToString());

            creditCardRepositoryMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetCreditCardSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(creditCard);

            specialOffers[0].SpecialOffer!.Type = "No Discount";
            specialOfferProductRepositoryMock.Setup(_ => _.ListAsync(
                It.IsAny<ISpecification<SpecialOfferProduct>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(specialOffers);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
            salesOrderIntegrationEventServiceMock.Verify(_ => _.AddAndSaveEventAsync(
                It.IsAny<IntegrationEvent>())
            );

            salesOrderRepositoryMock.Verify(_ => _.AddAsync(
                It.IsAny<SalesOrder>(),
                It.IsAny<CancellationToken>())
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnsTrueGivenCreditCardDoesNotExist(
            [Frozen] Mock<ISalesOrderIntegrationEventService> salesOrderIntegrationEventServiceMock,
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            [Frozen] Mock<IRepository<CreditCard>> creditCardRepositoryMock,
            [Frozen] Mock<IRepository<SpecialOfferProduct>> specialOfferProductRepositoryMock,
            List<SpecialOfferProduct> specialOffers,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange
            command.OrderItems?.ForEach(_ => _.UnitPriceDiscount = 0);

            creditCardRepositoryMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetCreditCardSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((CreditCard?)null);

            specialOffers[0].SpecialOffer!.Type = "No Discount";
            specialOfferProductRepositoryMock.Setup(_ => _.ListAsync(
                It.IsAny<ISpecification<SpecialOfferProduct>>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(specialOffers);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
            salesOrderIntegrationEventServiceMock.Verify(_ => _.AddAndSaveEventAsync(
                It.IsAny<IntegrationEvent>())
            );

            salesOrderRepositoryMock.Verify(_ => _.AddAsync(
                It.IsAny<SalesOrder>(),
                It.IsAny<CancellationToken>())
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowCustomerNotFoundExceptionGivenCustomerDoesNotExist(
            [Frozen] Mock<IRepository<Customer>> customerRepositoryMock,
            CreateSalesOrderCommandHandler sut,
            CreateSalesOrderCommand command
        )
        {
            //Arrange
            customerRepositoryMock.Setup(_ => _.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Customer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>();
        }
    }
}
