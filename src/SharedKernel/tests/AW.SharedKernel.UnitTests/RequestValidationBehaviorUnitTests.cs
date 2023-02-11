using AW.SharedKernel.UnitTesting;
using AW.SharedKernel.UnitTests.Mediatr;
using AW.SharedKernel.UnitTests.Validators;
using AW.SharedKernel.Validation;
using FluentAssertions;
using FluentValidation;
using Xunit;

namespace AW.SharedKernel.UnitTests
{
    public class RequestValidationBehaviorUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ValidValidator_ReturnsResponse(
            Customer customer,
            string customerNumber
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
                new GetCustomerQuery(customerNumber),
                () => Task.FromResult(customer),
                CancellationToken.None
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
                    new GetCustomerQuery(string.Empty),
                    () => Task.FromResult(new Customer()),
                    CancellationToken.None
                );

            //Assert
            await func.Should().ThrowAsync<ValidationException>()
                .Where(ex => ex.Errors.ToList().Count == 1);
        }
    }
}