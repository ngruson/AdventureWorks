CREATE TABLE [SpecialOfferProduct](
	[SpecialOfferProductID] [int] IDENTITY(1,1) NOT NULL,
	[SpecialOfferID] [int] NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL
 CONSTRAINT [PK_SpecialOfferProduct_SpecialOfferProductID] PRIMARY KEY CLUSTERED
(
	[SpecialOfferProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]