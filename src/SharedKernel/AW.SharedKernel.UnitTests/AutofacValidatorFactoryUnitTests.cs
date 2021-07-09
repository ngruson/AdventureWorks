using Autofac.Features.Indexed;
using AW.SharedKernel.Validation;
using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using Xunit;

namespace AW.SharedKernel.UnitTests
{
    public class AutofacValidatorFactoryUnitTests
    {
        [Fact]
        public void CreateInstance_ReturnValidator()
        {
            //Arrange
            var validator = new TestValidator();
            var indexMock = new Mock<IIndex<Type, IValidator>>();
            indexMock.Setup(x => x[It.IsAny<Type>()])
                .Returns(validator);

            //Act
            var factory = new AutofacValidatorFactory(indexMock.Object);
            var result = factory.CreateInstance(It.IsAny<Type>());

            //Assert
            result.Should().Be(validator);
        }
    }
}