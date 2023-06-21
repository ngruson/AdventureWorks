CREATE TABLE [PersonEmailAddress](
	[PersonEmailAddressID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[PersonID] [int] NOT NULL,
	[EmailAddress]  [nvarchar](50) NOT NULL
    CONSTRAINT [PK_PersonEmailAddress_PersonEmailAddressID] PRIMARY KEY CLUSTERED ([PersonEmailAddressID] ASC),
    CONSTRAINT [FK_PersonEmailAddress_Person_PersonID] FOREIGN KEY([PersonID]) REFERENCES [Person] ([PersonID]),
    CONSTRAINT UC_PersonEmailAddress_ObjectID UNIQUE(ObjectID)
) ON [PRIMARY]
GO