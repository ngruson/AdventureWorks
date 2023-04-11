IF NOT EXISTS (SELECT TOP 1 1 FROM [Location])
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Location...'

	SET IDENTITY_INSERT [Location] ON
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (1, N'Tool Crib', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (2, N'Sheet Metal Racks', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (3, N'Paint Shop', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (4, N'Paint Storage', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (5, N'Metal Storage', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (6, N'Miscellaneous Storage', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (7, N'Finished ods Storage', 0.0000, CAST(0.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (10, N'Frame Forming', 22.5000, CAST(96.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (20, N'Frame Welding', 25.0000, CAST(108.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (30, N'Debur and Polish', 14.5000, CAST(120.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (40, N'Paint', 15.7500, CAST(120.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (45, N'Specialized Paint', 18.0000, CAST(80.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (50, N'Subassembly', 12.2500, CAST(120.00 AS Decimal(8, 2)))
    
    INSERT [Location] ([LocationID], [Name], [CostRate], [Availability]) VALUES (60, N'Final Assembly', 12.2500, CAST(120.00 AS Decimal(8, 2)))    

    SET IDENTITY_INSERT [Location] OFF
END