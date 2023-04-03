using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.CreateProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class CreateProductCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task ReturnsNoValidationErrorGivenValidCommand(
            CreateProductCommandValidator sut,
            CreateProductCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [AutoMoqData]
        public async Task ReturnsValidationErrorGivenNameIsEmpty(
            CreateProductCommandValidator sut,
            CreateProductCommand command
        )
        {
            //Arrange
            command.Product!.Name = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Product!.Name)
                .WithErrorMessage("Name is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task ReturnsValidationErrorGivenProductNumberIsEmpty(
            CreateProductCommandValidator sut,
            CreateProductCommand command
        )
        {
            //Arrange
            command.Product!.ProductNumber = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Product!.ProductNumber)
                .WithErrorMessage("Product number is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task ReturnsValidationErrorGivenProductAlreadyExists(
            [Frozen] Mock<IRepository<Entities.Product>> productRepo,
            CreateProductCommandValidator sut,
            CreateProductCommand command
        )
        {
            //Arrange
            productRepo.Setup(x => x.AnyAsync(
                It.IsAny<ProductExistsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Product!.ProductNumber)
                .WithErrorMessage("Product number already exists");
        }
    }
}
