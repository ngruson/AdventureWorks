using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateIndividualCustomerEmailAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task return_success_given_customer_exists(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            CreateIndividualCustomerEmailAddressCommandHandler sut,
            Entities.Person person,
            Guid customerId
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerEmailAddressCommand(
                customerId,
                EmailAddress.Create("test@test.com").Value
            );

            //Act
            customerRepoMock.Setup(_ =>
                _.SingleOrDefaultAsync(
                    It.IsAny<GetIndividualCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new Entities.IndividualCustomer(person));

            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_is_invalid(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            [Frozen] Mock<IValidator<CreateIndividualCustomerEmailAddressCommand>> validator,
            CreateIndividualCustomerEmailAddressCommandHandler sut,
            CreateIndividualCustomerEmailAddressCommand command,
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
                    It.IsAny<Entities.IndividualCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            CreateIndividualCustomerEmailAddressCommandHandler sut,
            Guid customerId
        )
        {
            // Arrange
            var command = new CreateIndividualCustomerEmailAddressCommand(
                customerId,
                EmailAddress.Create("test@test.com").Value
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.IndividualCustomer?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepo,
            [Frozen] Mock<IValidator<CreateIndividualCustomerEmailAddressCommand>> validator,
            CreateIndividualCustomerEmailAddressCommandHandler sut,
            Guid customerId
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerEmailAddressCommand(
                customerId,
                EmailAddress.Create("test@test.com").Value
            );

            validator.Setup(x => x.ValidateAsync(
                command,
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            customerRepo.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.IndividualCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
