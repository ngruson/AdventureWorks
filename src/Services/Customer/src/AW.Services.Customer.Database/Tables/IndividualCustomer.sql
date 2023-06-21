CREATE TABLE [IndividualCustomer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL
    CONSTRAINT [PK_IndividualCustomer_IndividualCustomerID] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_IndividualCustomer_Customer_CustomerID] FOREIGN KEY([CustomerID]) REFERENCES [Customer] ([CustomerID]),
    CONSTRAINT [FK_IndividualCustomer_Person_PersonID] FOREIGN KEY([PersonID]) REFERENCES [Person] ([PersonID])
)