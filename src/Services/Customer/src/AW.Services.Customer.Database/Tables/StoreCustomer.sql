CREATE TABLE [dbo].[StoreCustomer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SalesPerson] [nvarchar](50) NULL,
    CONSTRAINT [PK_StoreCustomer_CustomerID] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_StoreCustomer_Customer_CustomerID] FOREIGN KEY([CustomerID]) REFERENCES [Customer] ([CustomerID])
)
GO