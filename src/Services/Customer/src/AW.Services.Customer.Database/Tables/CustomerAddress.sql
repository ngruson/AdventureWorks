CREATE TABLE [CustomerAddress](
	[CustomerAddressID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[CustomerID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
	[AddressType] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_CustomerAddress_CustomerAddressID] PRIMARY KEY CLUSTERED ([CustomerAddressID] ASC),
    CONSTRAINT [FK_CustomerAddress_Customer_CustomerID] FOREIGN KEY([CustomerID]) REFERENCES [Customer] ([CustomerID]),
    CONSTRAINT [FK_CustomerAddress_Address_AddressID] FOREIGN KEY([AddressID]) REFERENCES [Address] ([AddressID]),
    CONSTRAINT UC_CustomerAddress_ObjectID UNIQUE(ObjectID)
)
GO