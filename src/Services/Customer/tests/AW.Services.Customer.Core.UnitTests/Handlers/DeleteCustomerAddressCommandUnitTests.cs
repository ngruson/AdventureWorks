using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteCustomerAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task return_success_given_customer_and_address_exist(
            [Frozen] Entities.IndividualCustomer customer,
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command,
            Entities.CustomerAddress address

        )
        {
            //Arrange
            customer.AddAddress(
                new Entities.CustomerAddress(
                    command.AddressId,
                    address.AddressType,
                    address.Address
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_is_invalid(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            [Frozen] Mock<IValidator<DeleteCustomerAddressCommand>> validator,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command,
            List<ValidationFailure> failures
        )
        {
            // Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new ValidationResult(failures));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);

            customerRepoMock.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.Customer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_address_does_not_exist(
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
        )
        {
            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IValidator<DeleteCustomerAddressCommand>> validator,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
        )
        {
            //Arrange
            validator.Setup(_ => _.ValidateAsync(
                    command,
                    It.IsAny<CancellationToken>()
                )
            )
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
