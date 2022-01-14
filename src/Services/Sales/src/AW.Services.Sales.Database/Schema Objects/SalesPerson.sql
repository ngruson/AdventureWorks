CREATE TABLE [SalesPerson](
	[SalesPersonID] [int] IDENTITY(1,1) NOT NULL,
	[Territory] [nvarchar](50) NULL
 CONSTRAINT [PK_SalesPerson_SalesPersonID] PRIMARY KEY CLUSTERED
(
	[SalesPersonID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [SalesPerson] WITH CHECK ADD CONSTRAINT [FK_SalesPerson_Person_PersonID] FOREIGN KEY([SalesPersonID])
REFERENCES [Person] ([PersonID])
GO

ALTER TABLE [SalesPerson] CHECK CONSTRAINT [FK_SalesPerson_Person_PersonID]
GO