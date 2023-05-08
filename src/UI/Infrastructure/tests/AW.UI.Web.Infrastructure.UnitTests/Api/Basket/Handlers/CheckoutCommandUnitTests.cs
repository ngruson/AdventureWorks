﻿using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.Basket.Handlers.Checkout;
using AW.UI.Web.Infrastructure.Api.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.Basket.Handlers
{
    public class CheckoutCommandUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_WithBasket_BasketIsCheckedOut(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            CheckoutCommandHandler sut,
            CheckoutCommand command
        )
        {
            //Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert

            mockBasketApiClient.Verify(_ => _.CheckoutAsync(
                    It.IsAny<BasketCheckout>()
                )
            );
        }

        [Theory, AutoMoqData]
        public async Task Handle_WithoutBasket_ThrowsArgumentException(
            [Frozen] Mock<IBasketApiClient> mockBasketApiClient,
            CheckoutCommandHandler sut
        )
        {
            //Arrange
            var query = new CheckoutCommand(null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'request.Basket')");

            mockBasketApiClient.Verify(_ => _.CheckoutAsync(
                    It.IsAny<BasketCheckout>()
                ),
                Times.Never
            );
        }
    }
}