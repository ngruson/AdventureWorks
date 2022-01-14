CREATE TABLE [SalesOrderLine](
	[SalesOrderLineID] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrderID] [int] NOT NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[OrderQty] [smallint] NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[SpecialOfferProductId] [int] NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[UnitPriceDiscount] [money] NOT NULL,
 CONSTRAINT [PK_SalesOrderLine_SalesOrderLineID] PRIMARY KEY CLUSTERED
(
	[SalesOrderLineID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]