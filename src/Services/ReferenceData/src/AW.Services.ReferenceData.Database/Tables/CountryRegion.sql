CREATE TABLE CountryRegion(
	CountryRegionCode [nvarchar](3) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	Name nvarchar(50) NOT NULL
    CONSTRAINT [PK_CountryRegion_CountryRegionCode] PRIMARY KEY CLUSTERED ( [CountryRegionCode] ASC ),
    CONSTRAINT UC_CountryRegion_ObjectID UNIQUE(ObjectID)
) ON [PRIMARY]
GO