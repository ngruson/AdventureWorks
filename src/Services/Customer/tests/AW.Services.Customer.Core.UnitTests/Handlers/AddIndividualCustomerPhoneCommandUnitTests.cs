using AutoFixture.Xunit2;
using AW.Services.Customer.Core.AutoMapper;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class AddIndividualCustomerPhoneCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CustomerExist_AddIndividualCustomerPhone(
            [Frozen] Mock<IRepository<Entities.IndividualCustomer>> customerRepoMock,
            AddIndividualCustomerPhoneCommandHandler sut,
            AddIndividualCustomerPhoneCommand command,
            Entities.Person person
        )
        {
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
            AddIndividualCustomerPhoneCommandHandler sut,
            AddIndividualCustomerPhoneCommand command
        )
        {
            // Arrange
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
    }
}