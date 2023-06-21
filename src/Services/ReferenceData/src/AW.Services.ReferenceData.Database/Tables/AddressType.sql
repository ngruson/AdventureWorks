CREATE TABLE AddressType(
	[AddressTypeID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_AddressType_AddressTypeID] PRIMARY KEY CLUSTERED ( [AddressTypeID] ASC ),
    CONSTRAINT UC_AddressType_ObjectID UNIQUE(ObjectID)
)