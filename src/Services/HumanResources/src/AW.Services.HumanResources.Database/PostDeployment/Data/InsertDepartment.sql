IF NOT EXISTS (SELECT TOP 1 1 FROM Department)
BEGIN	
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Department...'

	SET IDENTITY_INSERT [Department] ON

    INSERT [Department] ([DepartmentID],[Name],[GroupName])
    SELECT 1,N'Engineering',N'Research and Development' UNION ALL
    SELECT 2,N'Tool Design',N'Research and Development' UNION ALL
    SELECT 3,N'Sales',N'Sales and Marketing' UNION ALL
    SELECT 4,N'Marketing',N'Sales and Marketing' UNION ALL
    SELECT 5,N'Purchasing',N'Inventory Management' UNION ALL
    SELECT 6,N'Research and Development',N'Research and Development' UNION ALL
    SELECT 7,N'Production',N'Manufacturing' UNION ALL
    SELECT 8,N'Production Control',N'Manufacturing' UNION ALL
    SELECT 9,N'Human Resources',N'Executive General and Administration' UNION ALL
    SELECT 10,N'Finance',N'Executive General and Administration' UNION ALL
    SELECT 11,N'Information Services',N'Executive General and Administration' UNION ALL
    SELECT 12,N'Document Control',N'Quality Assurance' UNION ALL
    SELECT 13,N'Quality Assurance',N'Quality Assurance' UNION ALL
    SELECT 14,N'Facilities and Maintenance',N'Executive General and Administration' UNION ALL
    SELECT 15,N'Shipping and Receiving',N'Inventory Management' UNION ALL
    SELECT 16,N'Executive',N'Executive General and Administration';

	SET IDENTITY_INSERT [Department] OFF
END
GO