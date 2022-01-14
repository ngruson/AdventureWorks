IF NOT EXISTS (SELECT TOP 1 1 FROM SalesPerson)
BEGIN
	SET IDENTITY_INSERT [SalesPerson] ON

	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (274, NULL)
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (275, N'Northeast')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (276, N'Southwest')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (277, N'Central')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (278, N'Canada')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (279, N'Southeast')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (280, N'Northwest')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (281, N'Southwest')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (282, N'Canada')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (283, N'Northwest')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (284, N'Northwest')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (285, NULL)
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (286, N'Australia')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (287, NULL)
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (288, N'Germany')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (289, N'United Kingdom')
	
	INSERT [SalesPerson] ([SalesPersonID], [Territory]) VALUES (290, N'France')

	SET IDENTITY_INSERT [SalesPerson] OFF
END