CREATE TABLE [dbo].[Employee](
	[PersonID] [int] NOT NULL,
	[NationalIDNumber] [nvarchar](15) NOT NULL,
	[LoginID] [nvarchar](256) NOT NULL,
	[OrganizationNode] [hierarchyid] NULL,
	[OrganizationLevel] [smallint] NULL,
	[JobTitle] [nvarchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[MaritalStatus] [nchar](1) NOT NULL,
	[Gender] [nchar](1) NOT NULL,
	[HireDate] [date] NOT NULL,
	[SalariedFlag] bit NOT NULL,
	[CurrentFlag] bit NOT NULL,
	CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 	([PersonID] ASC),
	CONSTRAINT [FK_Employee_Person_PersonID] FOREIGN KEY([PersonID]) REFERENCES [Person] ([PersonID])
) ON [PRIMARY]
GO