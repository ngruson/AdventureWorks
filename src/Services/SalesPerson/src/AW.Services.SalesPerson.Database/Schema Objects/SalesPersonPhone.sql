CREATE TABLE [SalesPersonPhone](
	[SalesPersonPhoneID] [int] IDENTITY(1,1) NOT NULL,
	[SalesPersonID] [int] NOT NULL,
	[PhoneNumberType] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](25) NOT NULL
 CONSTRAINT [PK_SalesPersonPhone_SalesPersonPhoneID] PRIMARY KEY CLUSTERED
(
	[SalesPersonPhoneID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO