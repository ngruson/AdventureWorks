IF NOT EXISTS (SELECT TOP 1 1 FROM SalesPerson)
BEGIN
	SET IDENTITY_INSERT [SalesPerson] ON

	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (274, NULL, N'Stephen', N'Y', N'Jiang', NULL, NULL)
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (275, NULL, N'Michael', N'G', N'Blythe', NULL, N'Northeast')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (276, NULL, N'Linda', N'C', N'Mitchell', NULL, N'Southwest')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (277, NULL, N'Jillian', NULL, N'Carson', NULL, N'Central')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (278, NULL, N'Garrett', N'R', N'Vargas', NULL, N'Canada')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (279, NULL, N'Tsvi', N'Michael', N'Reiter', NULL, N'Southeast')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (280, NULL, N'Pamela', N'O', N'Ansman-Wolfe', NULL, N'Northwest')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (281, NULL, N'Shu', N'K', N'Ito', NULL, N'Southwest')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (282, NULL, N'José', N'Edvaldo', N'Saraiva', NULL, N'Canada')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (283, NULL, N'David', N'R', N'Campbell', NULL, N'Northwest')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (284, N'Mr.', N'Tete', N'A', N'Mensa-Annan', NULL, N'Northwest')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (285, N'Mr.', N'Syed', N'E', N'Abbas', NULL, NULL)
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (286, NULL, N'Lynn', N'N', N'Tsoflias', NULL, N'Australia')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (287, NULL, N'Amy', N'E', N'Alberts', NULL, NULL)
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (288, NULL, N'Rachel', N'B', N'Valdez', NULL, N'Germany')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (289, NULL, N'Jae', N'B', N'Pak', NULL, N'United Kingdom')
	INSERT [dbo].[SalesPerson] ([SalesPersonID], [Title], [FirstName], [MiddleName], [LastName], [Suffix], [Territory]) VALUES (290, NULL, N'Ranjit', N'R', N'Varkey Chudukatil', NULL, N'France')
	
	SET IDENTITY_INSERT [SalesPerson] OFF
	
END