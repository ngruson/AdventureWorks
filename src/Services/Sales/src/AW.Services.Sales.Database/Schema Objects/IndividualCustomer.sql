CREATE TABLE [IndividualCustomer](
	[IndividualCustomerID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL
 CONSTRAINT [PK_IndividualCustomer_IndividualCustomerID] PRIMARY KEY CLUSTERED
(
	[IndividualCustomerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [IndividualCustomer] WITH CHECK ADD CONSTRAINT [FK_IndividualCustomer_Customer_CustomerID] FOREIGN KEY([IndividualCustomerID])
REFERENCES [Customer] ([CustomerID])
GO

ALTER TABLE [IndividualCustomer] CHECK CONSTRAINT [FK_IndividualCustomer_Customer_CustomerID]
GO

ALTER TABLE [IndividualCustomer] WITH CHECK ADD CONSTRAINT [FK_IndividualCustomer_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [Person] ([PersonID])
GO

ALTER TABLE [IndividualCustomer] CHECK CONSTRAINT [FK_IndividualCustomer_Person_PersonID]
GO