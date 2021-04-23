CREATE TABLE [Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[PostalCode] [nvarchar](15) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[StateProvinceCode] [nvarchar](50) NOT NULL,
	[CountryRegionCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Address_AddressID] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO