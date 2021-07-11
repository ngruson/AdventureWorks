using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProductCategories;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductCategoriesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async void Handle_ProductCategoriesExists_ReturnProductCategories(
            List<Entities.ProductCategory> productCategories,
            [Frozen] Mock<IRepository<Entities.ProductCategory>> categoriesRepoMock,
            GetProductCategoriesQueryHandler sut,
            GetProductCategoriesQuery query
        )
        {
            // Arrange
            categoriesRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductCategoriesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(productCategories);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            categoriesRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.ProductCategory>>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(productCategories[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_NoProductCategoriesExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.ProductCategory>> categoriesRepoMock,
            GetProductCategoriesQueryHandler sut,
            GetProductCategoriesQuery query
        )
        {
            //Arrange
            categoriesRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductCategoriesSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.ProductCategory>)null);

            //Act
            Func<Task> func = async() => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            categoriesRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.ProductCategory>>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}