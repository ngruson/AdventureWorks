using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteIndividualCustomerPhoneCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomerAndAddress_DeleteCustomerAddress(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            Entities.Person person,
            DeleteIndividualCustomerPhoneCommandHandler sut,
            DeleteIndividualCustomerPhoneCommand command
        )
        {
            //Arrange
            person.AddPhoneNumber(
                new Entities.PersonPhone(
                    command.Phone.PhoneNumberType!,
                    command.Phone.PhoneNumber!
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Entities.IndividualCustomer(person));

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            DeleteIndividualCustomerPhoneCommandHandler sut,
            DeleteIndividualCustomerPhoneCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.IndividualCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {command.AccountNumber} not found");
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_PhoneNumberDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            DeleteIndividualCustomerPhoneCommandHandler sut,
            DeleteIndividualCustomerPhoneCommand command,
            Entities.Person person
        )
        {
            //Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Entities.IndividualCustomer(person));

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<PhoneNumberNotFoundException>()
                .WithMessage($"Phone number (number: {command.Phone.PhoneNumber}, type: {command.Phone.PhoneNumberType}) for customer {command.AccountNumber} not found");
        }
    }
}