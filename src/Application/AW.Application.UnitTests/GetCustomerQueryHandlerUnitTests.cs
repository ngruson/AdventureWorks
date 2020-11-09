﻿using AW.Application.Customer.GetCustomer;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetCustomerQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_CustomerExists_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new GetCustomerQueryHandler(
                customerRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCustomerQuery
            {
                AccountNumber = customer.AccountNumber,
            };

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}