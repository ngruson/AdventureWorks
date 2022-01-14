CREATE TABLE [dbo].[StoreCustomer](
	[StoreCustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SalesPersonID] [int] NULL,
 CONSTRAINT [PK_StoreCustomer_StoreCustomerID] PRIMARY KEY CLUSTERED 
(
	[StoreCustomerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [StoreCustomer] WITH CHECK ADD CONSTRAINT [FK_StoreCustomer_Customer_CustomerID] FOREIGN KEY([StoreCustomerID])
REFERENCES [Customer] ([CustomerID])
GO

ALTER TABLE [StoreCustomer] CHECK CONSTRAINT [FK_StoreCustomer_Customer_CustomerID]
GO

ALTER TABLE [StoreCustomer] WITH CHECK ADD CONSTRAINT [FK_StoreCustomer_SalesPerson_SalesPersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [SalesPerson] ([SalesPersonID])
GO

ALTER TABLE [StoreCustomer] CHECK CONSTRAINT [FK_StoreCustomer_SalesPerson_SalesPersonID]
GO