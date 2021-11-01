CREATE TABLE [Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Title]  [nvarchar](8) NULL,
	[FirstName]  [nvarchar](50) NOT NULL,
	[MiddleName]  [nvarchar](50) NULL,
	[LastName]  [nvarchar](50) NOT NULL,
	[Suffix]  [nvarchar](10) NULL
 CONSTRAINT [PK_Person_PersonID] PRIMARY KEY CLUSTERED
(
	[PersonID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO