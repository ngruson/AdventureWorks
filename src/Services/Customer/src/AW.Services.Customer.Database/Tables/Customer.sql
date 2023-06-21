CREATE TABLE [Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Territory] [nvarchar](50) NULL,
	[AccountNumber]  [varchar](10) NOT NULL
    CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY CLUSTERED 
    (
	    [CustomerID] ASC
    ),
    CONSTRAINT UC_Customer_ObjectID UNIQUE(ObjectID)
) ON [PRIMARY]
GO