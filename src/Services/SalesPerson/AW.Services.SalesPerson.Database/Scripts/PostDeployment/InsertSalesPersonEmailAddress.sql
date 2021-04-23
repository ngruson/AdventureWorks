IF NOT EXISTS (SELECT TOP 1 1 FROM SalesPersonEmailAddress)
BEGIN
	SET IDENTITY_INSERT [SalesPersonEmailAddress] ON

	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (274, 274, N'stephen0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (275, 275, N'michael9@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (276, 276, N'linda3@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (277, 277, N'jillian0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (278, 278, N'garrett1@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (279, 279, N'tsvi0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (280, 280, N'pamela0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (281, 281, N'shu0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (282, 282, N'josé1@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (283, 283, N'david8@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (284, 284, N'tete0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (285, 285, N'syed0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (286, 286, N'lynn0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (287, 287, N'amy0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (288, 288, N'rachel0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (289, 289, N'jae0@adventure-works.com')
	INSERT [dbo].[SalesPersonEmailAddress] ([SalesPersonEmailAddressID], [SalesPersonID], [EmailAddress]) VALUES (290, 290, N'ranjit0@adventure-works.com')
	
	SET IDENTITY_INSERT [SalesPersonEmailAddress] OFF
END