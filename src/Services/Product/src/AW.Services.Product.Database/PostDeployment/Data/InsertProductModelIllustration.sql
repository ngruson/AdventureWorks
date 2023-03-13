IF NOT EXISTS (SELECT TOP 1 1 FROM ProductModelIllustration)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table ProductModelIllustration...'

    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (7, 3)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (10, 3)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (47, 4)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (47, 5)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (48, 4)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (48, 5)
    INSERT [dbo].[ProductModelIllustration] ([ProductModelID], [IllustrationID]) VALUES (67, 6)
END