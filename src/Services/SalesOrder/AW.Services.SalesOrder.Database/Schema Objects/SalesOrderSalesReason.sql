﻿CREATE TABLE [SalesOrderSalesReason](
	[SalesOrderSalesReasonID] [int] IDENTITY(1,1) NOT NULL,
	[SalesOrderID] [int] NOT NULL,
	[SalesReasonID] [int] NOT NULL
 CONSTRAINT [PK_SalesOrderSalesReason_SalesOrderSalesReasonID] PRIMARY KEY CLUSTERED
(
	[SalesOrderSalesReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]