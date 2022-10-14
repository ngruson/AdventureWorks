IF NOT EXISTS (SELECT TOP 1 1 FROM SalesPerson)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table SalesPerson...'

	SET IDENTITY_INSERT [SalesPerson] ON

	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (274, NULL)
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (275, N'Northeast')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (276, N'Southwest')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (277, N'Central')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (278, N'Canada')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (279, N'Southeast')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (280, N'Northwest')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (281, N'Southwest')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (282, N'Canada')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (283, N'Northwest')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (284, N'Northwest')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (285, NULL)
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (286, N'Australia')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (287, NULL)
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (288, N'Germany')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (289, N'United Kingdom')
	
	INSERT [SalesPerson] ([PersonID], [Territory]) VALUES (290, N'France')

	SET IDENTITY_INSERT [SalesPerson] OFF
END
GO