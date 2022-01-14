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

ALTER TABLE [SalesPersonPhone] WITH CHECK ADD CONSTRAINT [FK_SalesPersonPhone_SalesPerson_SalesPersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [SalesPerson] ([SalesPersonID])
GO

ALTER TABLE [SalesPersonPhone] CHECK CONSTRAINT [FK_SalesPersonPhone_SalesPerson_SalesPersonID]
GO