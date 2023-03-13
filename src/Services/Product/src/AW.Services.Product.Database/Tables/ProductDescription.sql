CREATE TABLE [ProductDescription](
	[ProductDescriptionID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](400) NOT NULL,
    CONSTRAINT [PK_ProductDescription_ProductDescriptionID] PRIMARY KEY CLUSTERED 
    (
	    [ProductDescriptionID] ASC
    )
)