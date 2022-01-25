CREATE TABLE [IndividualCustomer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	CONSTRAINT [PK_IndividualCustomer_CustomerID] PRIMARY KEY ([CustomerID]),
	CONSTRAINT [FK_IndividualCustomer_Customer_CustomerID] FOREIGN KEY ([CustomerID])
				REFERENCES [Customer] ([CustomerID]) ON DELETE NO ACTION,
	CONSTRAINT [FK_IndividualCustomer_Person_PersonID] FOREIGN KEY ([PersonID])
				REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
)