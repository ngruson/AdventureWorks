CREATE TABLE [SalesPersonEmailAddress](
	[SalesPersonEmailAddressID] [int] IDENTITY(1,1) NOT NULL,
	[SalesPersonID] [int] NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_SalesPersonEmailAddress_SalesPersonEmailAddressID] PRIMARY KEY CLUSTERED
(
	[SalesPersonEmailAddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO