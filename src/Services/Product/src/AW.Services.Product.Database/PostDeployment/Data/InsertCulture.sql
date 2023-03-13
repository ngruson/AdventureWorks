IF NOT EXISTS (SELECT TOP 1 1 FROM Culture)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Culture...'

	SET IDENTITY_INSERT [Culture] ON

    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'      ', N'Invariant Language (Invariant Country)')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'ar    ', N'Arabic')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'en    ', N'English')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'es    ', N'Spanish')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'fr    ', N'French')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'he    ', N'Hebrew')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'th    ', N'Thai')
    INSERT [dbo].[Culture] ([CultureID], [Name]) VALUES (N'zh-cht', N'Chinese')
    
    SET IDENTITY_INSERT [Culture] OFF
END