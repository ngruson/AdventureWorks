CREATE TABLE [dbo].[StoreCustomerContact](
	[StoreCustomerContactID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ContactType] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_StoreCustomerContact_StoreCustomerContactID] PRIMARY KEY CLUSTERED 
(
	[StoreCustomerContactID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO