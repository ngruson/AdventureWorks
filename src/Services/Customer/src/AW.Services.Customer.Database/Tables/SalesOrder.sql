CREATE TABLE [SalesOrder](
	[SalesOrderID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[OrderDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ShipDate] [datetime] NULL,
	[Status] [tinyint] NOT NULL,
	[OnlineOrderFlag] [bit] NOT NULL,
	[SalesOrderNumber] [nvarchar](25) NOT NULL,
	[PurchaseOrderNumber] [nvarchar](50) NULL,
	[AccountNumber] [nvarchar](50) NULL,
	[CustomerID] [int] NOT NULL,
	[TotalDue] [money] NOT NULL
    CONSTRAINT [PK_SalesOrder_SalesOrderID] PRIMARY KEY CLUSTERED ([SalesOrderID] ASC),
    CONSTRAINT [FK_SalesOrder_Customer_CustomerID] FOREIGN KEY([CustomerID]) REFERENCES [Customer] ([CustomerID]),
    CONSTRAINT UC_SalesOrder_ObjectID UNIQUE(ObjectID)
)