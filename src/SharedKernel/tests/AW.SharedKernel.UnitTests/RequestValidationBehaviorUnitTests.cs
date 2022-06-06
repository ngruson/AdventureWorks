using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.UnitTests.Mediatr;
using AW.SharedKernel.UnitTests.Validators;
using AW.SharedKernel.Validation;
using FluentAssertions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.SharedKernel.UnitTests
{
    public class RequestValidationBehaviorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ValidValidator_ReturnsResponse(
            Customer customer
        )
        {
            //Arrange
            var sut = new RequestValidationBehavior<GetCustomerQuery, Customer>(
                new List<IValidator<GetCustomerQuery>>
                {
                    new GetCustomerQueryValidator()
                }
            );

            //Act
            
            var result = await sut.Handle(
                new GetCustomerQuery { CustomerNumber = "1" },
                CancellationToken.None,
                () => Task.FromResult(customer)
            );

            //Assert
            result.Should().Be(customer);
        }

        [Fact]
        public async Task Handle_InvalidValidator_ThrowsValidationException()
        {
            //Arrange
            var sut = new RequestValidationBehavior<GetCustomerQuery, Customer>(
                new List<IValidator<GetCustomerQuery>>
                {
                    new GetCustomerQueryValidator()
                }
            );

            //Act
            Func<Task> func = async () => 
                await sut.Handle(
                    new GetCustomerQuery(),
                    CancellationToken.None,
                    () => Task.FromResult(new Customer())
                );

            //Assert
            await func.Should().ThrowAsync<ValidationException>()
                .Where(ex => ex.Errors.ToList().Count == 1);
        }
    }
}