CREATE TABLE [StateProvince](
	[StateProvinceID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[StateProvinceCode] [nchar](3) NOT NULL,
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[IsOnlyStateProvinceFlag] bit NOT NULL,
	[Name] [nvarchar](50) NOT NULL
	CONSTRAINT [PK_StateProvince_StateProvinceID] PRIMARY KEY CLUSTERED ( [StateProvinceID] ASC	),
    CONSTRAINT [FK_StateProvince_CountryRegion_CountryRegionCode] FOREIGN KEY ([CountryRegionCode])
	    REFERENCES [CountryRegion] ([CountryRegionCode]) ON DELETE NO ACTION,
    CONSTRAINT UC_StateProvince_ObjectID UNIQUE(ObjectID)
)