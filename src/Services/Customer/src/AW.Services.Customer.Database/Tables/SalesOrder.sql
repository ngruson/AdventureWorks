CREATE TABLE [SalesOrder](
	[SalesOrderID] [int] IDENTITY(1,1) NOT NULL,
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
 CONSTRAINT [PK_SalesOrder_SalesOrderID] PRIMARY KEY CLUSTERED 
(
	[SalesOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]