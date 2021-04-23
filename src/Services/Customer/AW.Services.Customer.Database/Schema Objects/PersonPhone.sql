CREATE TABLE [PersonPhone](
	[PersonPhoneID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[PhoneNumberType] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](25) NOT NULL
 CONSTRAINT [PK_PersonPhone_PersonPhoneID] PRIMARY KEY CLUSTERED
(
	[PersonPhoneID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO