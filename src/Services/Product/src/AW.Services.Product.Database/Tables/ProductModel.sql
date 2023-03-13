CREATE TABLE [dbo].[ProductModel]
(
	[ProductModelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CatalogDescription] [xml](CONTENT [ProductDescriptionSchemaCollection]) NULL,
	[Instructions] [xml](CONTENT [ManuInstructionsSchemaCollection]) NULL
CONSTRAINT [PK_ProductModel_ProductModelID] PRIMARY KEY CLUSTERED 
(
	[ProductModelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
