CREATE TABLE [dbo].[StoreCustomerContact](
	[StoreCustomerContactID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[CustomerID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ContactType] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_StoreCustomerContact_StoreCustomerContactID] PRIMARY KEY CLUSTERED ([StoreCustomerContactID] ASC),
    CONSTRAINT [FK_StoreCustomerContact_Customer_CustomerID] FOREIGN KEY([CustomerID]) REFERENCES [Customer] ([CustomerID]),
    CONSTRAINT [FK_StoreCustomerContact_Person_PersonID] FOREIGN KEY([PersonID]) REFERENCES [Person] ([PersonID]),
    CONSTRAINT UC_StoreCustomerContact_ObjectID UNIQUE(ObjectID)
)
GO