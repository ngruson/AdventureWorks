CREATE TABLE [dbo].[StoreCustomer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SalesPersonID] [int] NULL,
	CONSTRAINT [PK_StoreCustomer_CustomerID] PRIMARY KEY ([CustomerID]),
	CONSTRAINT [FK_StoreCustomer_Customer_CustomerID] FOREIGN KEY ([CustomerID])
				REFERENCES [Customer] ([CustomerID]) ON DELETE NO ACTION,
	CONSTRAINT [FK_StoreCustomer_SalesPerson_PersonID] FOREIGN KEY ([SalesPersonID])
				REFERENCES [SalesPerson] ([PersonID]) ON DELETE NO ACTION
)