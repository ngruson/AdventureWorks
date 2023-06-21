using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.CreateCustomer;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateCustomerCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_store_customer_was_created(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            CreateCustomerCommandHandler sut,
            StoreCustomer customer
        )
        {
            // Arrange
            
            //Act
            var result = await sut.Handle(
                new CreateCustomerCommand(customer),
                CancellationToken.None
            );

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));

            result.Value.Should().BeEquivalentTo(customer);
        }

        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_individual_customer_was_created(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            CreateCustomerCommandHandler sut,
            IndividualCustomer customer
        )
        {
            // Arrange
            var command = new CreateCustomerCommand(customer);

            //Act
            var result = await sut.Handle(
                command,
                CancellationToken.None
            );

            //Assert
            result.IsSuccess.Should().BeTrue();

            customerRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));

            result.Value.Should().BeEquivalentTo(customer);
        }

        [Theory, AutoMoqData]
        public async Task return_invalid_given_command_is_invalid(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            [Frozen] Mock<IValidator<CreateCustomerCommand>> validator,
            CreateCustomerCommandHandler sut,
            StoreCustomer customer,
            List<ValidationFailure> failures
        )
        {
            // Arrange
            var command = new CreateCustomerCommand(customer);

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

            customerRepoMock.Verify(x => x.AddAsync(
                    It.IsAny<Entities.Customer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory]
        [AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            CreateCustomerCommandHandler sut,
            StoreCustomer customer
        )
        {
            // Arrange
            var command = new CreateCustomerCommand(customer);

            customerRepoMock.Setup(x => x.AddAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new Exception());

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);
        }
    }
}
