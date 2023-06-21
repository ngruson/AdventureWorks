CREATE TABLE [PersonPhone](
	[PersonPhoneID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[PersonID] [int] NOT NULL,
	[PhoneNumberType] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](25) NOT NULL
    CONSTRAINT [PK_PersonPhone_PersonPhoneID] PRIMARY KEY CLUSTERED ([PersonPhoneID] ASC),
    CONSTRAINT [FK_PersonPhone_Person_PersonID] FOREIGN KEY([PersonID]) REFERENCES [Person] ([PersonID]),
    CONSTRAINT UC_PersonPhone_ObjectID UNIQUE(ObjectID)
)
GO