CREATE TABLE [PersonEmailAddress](
	[PersonEmailAddressID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[EmailAddress]  [nvarchar](50) NOT NULL
 CONSTRAINT [PK_PersonEmailAddress_PersonEmailAddressID] PRIMARY KEY CLUSTERED
(
	[PersonEmailAddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO