CREATE TABLE [Territory](
	[TerritoryID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] [nvarchar](50) NOT NULL,
	[CountryRegionCode] [nvarchar](3) NOT NULL,
	[Group] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_Territory_TerritoryID] PRIMARY KEY CLUSTERED ( [TerritoryID] ASC ),
    CONSTRAINT UC_Territory_ObjectID UNIQUE(ObjectID)
)