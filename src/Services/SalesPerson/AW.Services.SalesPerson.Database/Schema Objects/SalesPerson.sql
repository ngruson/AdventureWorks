CREATE TABLE [SalesPerson](
	[SalesPersonID] [int] IDENTITY(1,1) NOT NULL,
	[Title]  [nvarchar](8) NULL,
	[FirstName]  [nvarchar](50) NOT NULL,
	[MiddleName]  [nvarchar](50) NULL,
	[LastName]  [nvarchar](50) NOT NULL,
	[Suffix]  [nvarchar](10) NULL,
	[Territory] [nvarchar](50) NULL
 CONSTRAINT [PK_SalesPerson_SalesPersonID] PRIMARY KEY CLUSTERED
(
	[SalesPersonID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO