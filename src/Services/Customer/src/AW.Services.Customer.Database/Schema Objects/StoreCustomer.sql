CREATE TABLE [dbo].[StoreCustomer](
	[StoreCustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[SalesPerson] [nvarchar](50) NULL,
 CONSTRAINT [PK_StoreCustomer_StoreCustomerID] PRIMARY KEY CLUSTERED 
(
	[StoreCustomerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO