CREATE TABLE ContactType(
	[ContactTypeID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] [nvarchar](50) NOT NULL
    CONSTRAINT [PK_ContactType_ContactTypeID] PRIMARY KEY CLUSTERED ( [ContactTypeID] ASC ),
    CONSTRAINT UC_ContactType_ObjectID UNIQUE(ObjectID)
) ON [PRIMARY]