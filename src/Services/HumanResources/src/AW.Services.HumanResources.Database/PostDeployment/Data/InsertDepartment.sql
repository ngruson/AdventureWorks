IF NOT EXISTS (SELECT TOP 1 1 FROM Department)
BEGIN
	SET IDENTITY_INSERT [Department] ON

	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (1, N'Engineering', N'Research and Development', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (2, N'Tool Design', N'Research and Development', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (3, N'Sales', N'Sales and Marketing', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (4, N'Marketing', N'Sales and Marketing', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (5, N'Purchasing', N'Inventory Management', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (6, N'Research and Development', N'Research and Development', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (7, N'Production', N'Manufacturing', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (8, N'Production Control', N'Manufacturing', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (9, N'Human Resources', N'Executive General and Administration', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (10, N'Finance', N'Executive General and Administration', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (11, N'Information Services', N'Executive General and Administration', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (12, N'Document Control', N'Quality Assurance', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (13, N'Quality Assurance', N'Quality Assurance', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (14, N'Facilities and Maintenance', N'Executive General and Administration', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (15, N'Shipping and Receiving', N'Inventory Management', GETDATE())
	
	INSERT [Department] ([DepartmentID], [Name], [GroupName], [ModifiedDate]) VALUES (16, N'Executive', N'Executive General and Administration', GETDATE())

	SET IDENTITY_INSERT [Department] OFF
END