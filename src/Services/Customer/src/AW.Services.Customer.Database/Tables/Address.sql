CREATE TABLE [Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[PostalCode] [nvarchar](15) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[StateProvinceCode] [nvarchar](50) NULL,
	[CountryRegionCode] [nvarchar](50) NOT NULL,
    CONSTRAINT [PK_Address_AddressID] PRIMARY KEY CLUSTERED 
    (
	    [AddressID] ASC
    ),
    CONSTRAINT UC_Address_ObjectID UNIQUE(ObjectID)
)
GO