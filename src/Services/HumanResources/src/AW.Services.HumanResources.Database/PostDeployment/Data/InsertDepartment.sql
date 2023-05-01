IF NOT EXISTS (SELECT TOP 1 1 FROM Department)
BEGIN	
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Department...'

	SET IDENTITY_INSERT [Department] ON

	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (1, N'Engineering', N'Research and Development')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (2, N'Tool Design', N'Research and Development')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (3, N'Sales', N'Sales and Marketing')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (4, N'Marketing', N'Sales and Marketing')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (5, N'Purchasing', N'Inventory Management')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (6, N'Research and Development', N'Research and Development')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (7, N'Production', N'Manufacturing')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (8, N'Production Control', N'Manufacturing')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (9, N'Human Resources', N'Executive General and Administration')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (10, N'Finance', N'Executive General and Administration')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (11, N'Information Services', N'Executive General and Administration')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (12, N'Document Control', N'Quality Assurance')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (13, N'Quality Assurance', N'Quality Assurance')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (14, N'Facilities and Maintenance', N'Executive General and Administration')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (15, N'Shipping and Receiving', N'Inventory Management')
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName]) VALUES (16, N'Executive', N'Executive General and Administration')

	SET IDENTITY_INSERT [Department] OFF
END
GO