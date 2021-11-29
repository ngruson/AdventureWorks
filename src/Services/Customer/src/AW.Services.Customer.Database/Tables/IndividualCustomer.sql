﻿CREATE TABLE [IndividualCustomer](
	[IndividualCustomerID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL
 CONSTRAINT [PK_IndividualCustomer_IndividualCustomerID] PRIMARY KEY CLUSTERED
(
	[IndividualCustomerID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO