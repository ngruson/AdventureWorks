IF NOT EXISTS (SELECT TOP 1 1 FROM SalesReason)
BEGIN
	SET IDENTITY_INSERT [SalesReason] ON

	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (1, N'Price', N'Other')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (2, N'On Promotion', N'Promotion')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (3, N'Magazine Advertisement', N'Marketing')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (4, N'Television  Advertisement', N'Marketing')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (5, N'Manufacturer', N'Other')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (6, N'Review', N'Other')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (7, N'Demo Event', N'Marketing')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (8, N'Sponsorship', N'Marketing')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (9, N'Quality', N'Other')
	INSERT [SalesReason] ([SalesReasonID], [Name], [ReasonType]) VALUES (10, N'Other', N'Other')

	SET IDENTITY_INSERT [SalesReason] OFF
END