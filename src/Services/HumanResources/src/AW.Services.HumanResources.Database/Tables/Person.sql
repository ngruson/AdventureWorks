CREATE TABLE [Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Title]  [nvarchar](8) NULL,
	[FirstName]  [nvarchar](50) NOT NULL,
	[MiddleName]  [nvarchar](50) NULL,
	[LastName]  [nvarchar](50) NOT NULL,
	[Suffix]  [nvarchar](10) NULL
    CONSTRAINT [PK_Person_PersonID] PRIMARY KEY CLUSTERED ([PersonID] ASC),
    CONSTRAINT UC_Person_ObjectID UNIQUE(ObjectID)
)