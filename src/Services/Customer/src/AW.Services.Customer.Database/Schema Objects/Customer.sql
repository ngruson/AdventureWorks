CREATE TABLE [Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Territory] [nvarchar](50) NULL,
	[AccountNumber]  [varchar](10) NOT NULL
 CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO