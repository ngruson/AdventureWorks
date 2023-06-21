using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
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
    public class DeleteStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task return_success_given_customer_and_contact_exist(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            Entities.StoreCustomer customer,
            Entities.StoreCustomerContact contact,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
            customer.AddContact(
                new Entities.StoreCustomerContact(
                    command.ContactId,
                    contact.ContactType,
                    contact.ContactPerson
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_is_invalid(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepo,
            [Frozen] Mock<IValidator<DeleteStoreCustomerContactCommand>> validator,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command,
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

            customerRepo.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.StoreCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_customer_does_not_exist(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.NotFound);
        }

        [Theory]
        [AutoMoqData]
        public async Task return_notfound_given_contact_does_not_exist(
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
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
            [Frozen] Mock<IValidator<DeleteStoreCustomerContactCommand>> validator,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
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
