using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteIndividualCustomerEmailAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomer_DeleteEmailAddress(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            Entities.Person person,
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            string accountNumber
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerEmailAddressCommand
            {
                AccountNumber = accountNumber,
                EmailAddress = EmailAddress.Create("test@test.com").Value
            };

            person.AddEmailAddress(
                new Entities.PersonEmailAddress(command.EmailAddress)
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
            customerRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.IndividualCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            string accountNumber
        )
        {
            // Arrange
            var command = new DeleteIndividualCustomerEmailAddressCommand
            {
                AccountNumber = accountNumber,
                EmailAddress = EmailAddress.Create("test@test.com").Value
            };

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetIndividualCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.IndividualCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {command.AccountNumber} not found");
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_EmailAddressDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            DeleteIndividualCustomerEmailAddressCommandHandler sut,
            Entities.Person person,
            string accountNumber
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerEmailAddressCommand
            {
                AccountNumber = accountNumber,
                EmailAddress = EmailAddress.Create("test@test.com").Value
            };

            customerRepoMock.Setup(_ =>
                _.SingleOrDefaultAsync(
                    It.IsAny<GetIndividualCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(new Entities.IndividualCustomer(person));

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<EmailAddressNotFoundException>()
                .WithMessage($"Email address {command.EmailAddress.Value} for customer {command.AccountNumber} not found");
        }
    }
}