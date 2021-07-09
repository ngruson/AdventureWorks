CREATE TABLE [CustomerAddress](
	[CustomerAddressID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[AddressType] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_CustomerAddress_CustomerAddressID] PRIMARY KEY CLUSTERED 
(
	[CustomerAddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO