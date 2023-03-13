CREATE TABLE [ProductModelProductDescriptionCulture](
	[ProductModelProductDescriptionCultureID] [int] IDENTITY(1,1) NOT NULL,
    [ProductModelID] [int] NOT NULL,    
	[ProductDescriptionID] [int] NOT NULL,
	[CultureID] [nvarchar](6) NOT NULL,
    CONSTRAINT [PK_ProductModelProductDescriptionCulture_ProductModelProductDescriptionCultureID] PRIMARY KEY ([ProductModelProductDescriptionCultureID]),
    CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductModel_ProductModelID] FOREIGN KEY ([ProductModelID])
	    REFERENCES [ProductModel] ([ProductModelID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductModelProductDescriptionCulture_ProductDescription_ProductDescriptionID] FOREIGN KEY ([ProductDescriptionID])
	    REFERENCES [ProductDescription] ([ProductDescriptionID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductModelProductDescriptionCulture_Culture_CultureID] FOREIGN KEY ([CultureID])
	    REFERENCES [Culture] ([CultureID]) ON DELETE NO ACTION
)