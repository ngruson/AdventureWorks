using Ardalis.Result;
using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact;
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
    public class CreateStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task return_success_given_customer_exists(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            CreateStoreCustomerContactCommandHandler sut,
            CreateStoreCustomerContactCommand command
        )
        {
            //Arrange

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
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            [Frozen] Mock<IValidator<CreateStoreCustomerContactCommand>> validator,
            CreateStoreCustomerContactCommandHandler sut,
            CreateStoreCustomerContactCommand command,
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
            CreateStoreCustomerContactCommandHandler sut,
            CreateStoreCustomerContactCommand command
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

        [Theory, AutoMoqData]
        public async Task return_error_given_exception_was_thrown(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepo,
            CreateStoreCustomerContactCommandHandler sut,
            CreateStoreCustomerContactCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Status.Should().Be(ResultStatus.Error);

            customerRepo.Verify(x => x.UpdateAsync(
                    It.IsAny<Entities.StoreCustomer>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
