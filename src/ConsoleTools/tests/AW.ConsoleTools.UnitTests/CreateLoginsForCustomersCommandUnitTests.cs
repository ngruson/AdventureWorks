using AutoFixture.Xunit2;
using AW.ConsoleTools.Handlers.CreateLoginsForCustomers;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.SharedKernel.UnitTesting;
using MediatR;
using Moq;
using Xunit;

namespace AW.ConsoleTools.UnitTests;

public class CreateLoginsForCustomersCommandUnitTests
{
    [Theory]
    [AutoMoqData]
    public async Task Handle_CustomerExists_ReturnCustomer(
        [Frozen] Mock<IMediator> mediatorMock,
        CreateLoginsForCustomersCommandHandler sut,
        CreateLoginsForCustomersCommand command,
        List<IndividualCustomer> customers
    )
    {
        //Arrange
        mediatorMock.Setup(_ => _.Send(
                It.IsAny<GetCustomersQuery>(),
                It.IsAny<CancellationToken>()
            )
        )
        .ReturnsAsync(
            customers.Cast<Customer?>().ToList()
        );

        //Act
        await sut.Handle(command, CancellationToken.None);

        //Assert
        mediatorMock.Verify(_ => _.Send(
                It.IsAny<CreateLoginCommand>(),
                It.IsAny<CancellationToken>()
            ), Times.Exactly(customers.Count)
        );
    }
}
