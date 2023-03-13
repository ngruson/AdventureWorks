CREATE TABLE [ProductModelIllustration](
	[ProductModelIllustrationID] [int] IDENTITY(1, 1) NOT NULL,
    [ProductModelID] [int] NOT NULL,
	[IllustrationID] [int] NOT NULL,
    CONSTRAINT [PK_ProductModelIllustration_ProductModelIllustrationID] PRIMARY KEY ([ProductModelIllustrationID]),
    CONSTRAINT [FK_ProductModelIllustration_ProductModel_ProductModelID] FOREIGN KEY ([ProductModelID])
	    REFERENCES [ProductModel] ([ProductModelID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductModelIllustration_Illustration_IllustrationID] FOREIGN KEY ([IllustrationID])
	    REFERENCES [Illustration] ([IllustrationID]) ON DELETE NO ACTION,
) ON [PRIMARY]
GO