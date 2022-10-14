CREATE TABLE [SalesOrder](
	[SalesOrderID] [int] IDENTITY(1,1) NOT NULL,
	[RevisionNumber] [tinyint] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NULL,
	[Status] [tinyint] NOT NULL,
	[OnlineOrderFlag] [bit] NOT NULL,
	[SalesOrderNumber]  AS (isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***')),
	[PurchaseOrderNumber] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[CustomerID] [int] NOT NULL,
	[SalesPersonID] [int] NULL,
	[Territory] [nvarchar](50) NULL,
	[BillToAddress_AddressLine1] [nvarchar](60) NOT NULL,
	[BillToAddress_AddressLine2] [nvarchar](60) NULL,
	[BillToAddress_PostalCode] [nvarchar](15) NOT NULL,
	[BillToAddress_City] [nvarchar](30) NOT NULL,
	[BillToAddress_StateProvinceCode] [nchar](3) NULL,
	[BillToAddress_CountryRegionCode] [nvarchar](3) NOT NULL,
	[ShipToAddress_AddressLine1] [nvarchar](60) NOT NULL,
	[ShipToAddress_AddressLine2] [nvarchar](60) NULL,
	[ShipToAddress_PostalCode] [nvarchar](15) NOT NULL,
	[ShipToAddress_City] [nvarchar](30) NOT NULL,
	[ShipToAddress_StateProvinceCode] [nchar](3) NULL,
	[ShipToAddress_CountryRegionCode] [nvarchar](3) NOT NULL,
	[ShipMethod] [nvarchar](50) NOT NULL,
	[TaxRate] [decimal] NULL,
	[CreditCardID] [int] NULL,
	[Freight] [money] NOT NULL,
	[Comment] [nvarchar](128) NULL
 CONSTRAINT [PK_SalesOrder_SalesOrderID] PRIMARY KEY CLUSTERED
(
	[SalesOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [SalesOrder] WITH CHECK ADD CONSTRAINT [FK_SalesOrder_Customer_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [Customer] ([CustomerID])
GO

ALTER TABLE [SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_Customer_CustomerID]
GO

ALTER TABLE [SalesOrder] WITH CHECK ADD CONSTRAINT [FK_SalesOrder_SalesPerson_SalesPersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [SalesPerson] ([PersonID])
GO

ALTER TABLE [SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_SalesPerson_SalesPersonID]
GO

ALTER TABLE [SalesOrder] WITH CHECK ADD CONSTRAINT [FK_SalesOrder_CreditCard_CreditCardID] FOREIGN KEY([CreditCardID])
REFERENCES [CreditCard] ([CreditCardID])
GO

ALTER TABLE [SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_CreditCard_CreditCardID]
GO