using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Exceptions;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using System;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Entities
{
    public class SalesOrderLineUnitTests
    {
        public class Create
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void Create_Ok_OrderLineCreated(
                string productNumber,
                string productName,
                decimal unitPrice,
                SpecialOfferProduct specialOfferProduct
            )
            {
                //Arrange

                //Act
                var salesOrderLine = new SalesOrderLine(
                    productNumber,
                    productName,
                    unitPrice,
                    unitPrice / 2,
                    specialOfferProduct.SpecialOffer!
                );

                //Assert
                salesOrderLine.Should().NotBeNull();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void Create_NegativeQuantity_ThrowSalesOrderDomainException(
                string productNumber,
                string productName,
                decimal unitPrice,
                decimal unitPriceDiscount,
                SpecialOfferProduct specialOfferProduct
            )
            {
                //Arrange

                //Act
                Action act = () => {
                    var salesOrderLine = new SalesOrderLine(
                        productNumber,
                        productName,
                        unitPrice,
                        unitPriceDiscount,
                        specialOfferProduct.SpecialOffer!,
                        - 1
                    );
                };

                //Assert
                act.Should().Throw<SalesDomainException>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void Create_ZeroQuantity_ThrowSalesOrderDomainException(
                string productNumber,
                string productName,
                decimal unitPrice,
                decimal unitPriceDiscount,
                SpecialOfferProduct specialOfferProduct
            )
            {
                //Arrange

                //Act
                Action act = () => {
                    var salesOrderLine = new SalesOrderLine(
                        productNumber,
                        productName,
                        unitPrice,
                        unitPriceDiscount,
                        specialOfferProduct.SpecialOffer!,
                        0
                    );
                };

                //Assert
                act.Should().Throw<SalesDomainException>();
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void Create_DiscountTooHigh_ThrowSalesOrderDomainException(
                string productNumber,
                string productName,
                decimal unitPrice,
                SpecialOfferProduct specialOfferProduct
            )
            {
                //Arrange

                //Act
                Action act = () => {
                    var salesOrderLine = new SalesOrderLine(
                        productNumber,
                        productName,
                        unitPrice,
                        unitPrice + 1,
                        specialOfferProduct.SpecialOffer!
                    );
                };

                //Assert
                act.Should().Throw<SalesDomainException>();
            }
        }

        public class SetNewDiscount
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void SetNewDiscount_ZeroDiscount_DiscountUpdated(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange

                //Act
                salesOrderLine.SetNewDiscount(0);

                //Assert
                salesOrderLine.UnitPriceDiscount.Should().Be(0);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void SetNewDiscount_PositiveDiscount_DiscountUpdated(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange

                //Act
                salesOrderLine.SetNewDiscount(5);

                //Assert
                salesOrderLine.UnitPriceDiscount.Should().Be(5);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void SetNewDiscount_NegativeDiscount_ThrowSalesOrderDomainException(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange

                //Act
                Action act = () => {
                    salesOrderLine.SetNewDiscount(-1);
                };

                //Assert
                act.Should().Throw<SalesDomainException>();
            }
        }

        public class AddQuantity
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void AddQuantity_ZeroQuantity_QuantityUpdated(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange
                var originalQuantity = salesOrderLine.OrderQty;

                //Act
                salesOrderLine.AddQuantity(0);

                //Assert
                salesOrderLine.OrderQty.Should().Be(originalQuantity);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void AddQuantity_PositiveQuantity_QuantityUpdated(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange
                var originalQuantity = salesOrderLine.OrderQty;

                //Act
                salesOrderLine.AddQuantity(1);

                //Assert
                salesOrderLine.OrderQty.Should().Be((short)(originalQuantity + 1));
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public void AddQuantity_NegativeQuantity_ThrowSalesOrderDomainException(
                Core.Entities.SalesOrderLine salesOrderLine
            )
            {
                //Arrange

                //Act
                Action act = () => {
                    salesOrderLine.AddQuantity(-1);
                };

                //Assert
                act.Should().Throw<SalesDomainException>();
            }
        }
    }
}
