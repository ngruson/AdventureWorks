IF NOT EXISTS (SELECT TOP 1 1 FROM EmployeeDepartmentHistory)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table EmployeeDepartmentHistory...'

	INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (1, 16, 1, CAST(N'2009-01-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (2, 1, 1, CAST(N'2008-01-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (3, 1, 1, CAST(N'2007-11-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (4, 1, 1, CAST(N'2007-12-05' AS Date), CAST(N'2010-05-30' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (4, 2, 1, CAST(N'2010-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (5, 1, 1, CAST(N'2008-01-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (6, 1, 1, CAST(N'2008-01-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (7, 6, 1, CAST(N'2009-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (8, 6, 1, CAST(N'2008-12-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (9, 6, 1, CAST(N'2009-01-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (10, 6, 1, CAST(N'2009-05-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (11, 2, 1, CAST(N'2010-12-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (12, 2, 1, CAST(N'2007-12-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (13, 2, 1, CAST(N'2010-12-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (14, 1, 1, CAST(N'2010-12-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (15, 1, 1, CAST(N'2011-01-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (16, 5, 1, CAST(N'2007-12-20' AS Date), CAST(N'2009-07-14' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (16, 4, 1, CAST(N'2009-07-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (17, 4, 1, CAST(N'2007-01-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (18, 4, 1, CAST(N'2011-02-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (19, 4, 1, CAST(N'2011-02-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (20, 4, 1, CAST(N'2011-01-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (21, 4, 1, CAST(N'2009-03-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (22, 4, 1, CAST(N'2008-12-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (23, 4, 1, CAST(N'2009-01-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (24, 4, 1, CAST(N'2009-01-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (25, 7, 1, CAST(N'2009-02-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (26, 8, 1, CAST(N'2008-12-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (27, 7, 1, CAST(N'2008-02-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (28, 7, 1, CAST(N'2006-06-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (29, 7, 1, CAST(N'2009-01-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (30, 7, 1, CAST(N'2009-01-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (31, 7, 1, CAST(N'2009-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (32, 7, 1, CAST(N'2008-12-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (33, 7, 1, CAST(N'2008-12-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (34, 7, 1, CAST(N'2009-02-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (35, 7, 1, CAST(N'2009-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (36, 7, 1, CAST(N'2009-02-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (37, 7, 1, CAST(N'2009-03-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (38, 7, 1, CAST(N'2010-01-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (39, 7, 1, CAST(N'2010-02-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (40, 7, 3, CAST(N'2007-12-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (41, 7, 3, CAST(N'2009-01-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (42, 7, 3, CAST(N'2008-12-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (43, 7, 3, CAST(N'2009-01-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (44, 7, 3, CAST(N'2008-12-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (45, 7, 3, CAST(N'2009-02-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (46, 7, 3, CAST(N'2009-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (47, 7, 1, CAST(N'2009-02-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (48, 7, 1, CAST(N'2008-01-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (49, 7, 1, CAST(N'2008-01-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (50, 7, 1, CAST(N'2008-02-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (51, 7, 1, CAST(N'2008-02-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (52, 7, 1, CAST(N'2008-03-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (53, 7, 1, CAST(N'2008-03-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (54, 7, 1, CAST(N'2010-01-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (55, 7, 3, CAST(N'2008-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (56, 7, 3, CAST(N'2009-02-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (57, 7, 3, CAST(N'2009-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (58, 7, 3, CAST(N'2008-12-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (59, 7, 3, CAST(N'2008-12-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (60, 7, 3, CAST(N'2009-01-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (61, 7, 3, CAST(N'2009-01-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (62, 7, 2, CAST(N'2008-03-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (63, 7, 2, CAST(N'2010-01-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (64, 7, 2, CAST(N'2010-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (65, 7, 2, CAST(N'2010-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (66, 7, 2, CAST(N'2009-12-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (67, 7, 2, CAST(N'2009-03-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (68, 7, 2, CAST(N'2009-12-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (69, 7, 2, CAST(N'2009-02-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (70, 7, 2, CAST(N'2008-12-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (71, 7, 3, CAST(N'2009-02-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (72, 7, 3, CAST(N'2008-12-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (73, 7, 3, CAST(N'2008-12-19' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (74, 7, 3, CAST(N'2009-01-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (75, 7, 3, CAST(N'2009-01-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (76, 7, 3, CAST(N'2009-02-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (77, 7, 3, CAST(N'2009-03-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (78, 7, 2, CAST(N'2008-12-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (79, 7, 2, CAST(N'2010-01-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (80, 7, 2, CAST(N'2010-01-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (81, 7, 2, CAST(N'2009-12-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (82, 7, 2, CAST(N'2010-03-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (83, 7, 2, CAST(N'2010-02-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (84, 7, 2, CAST(N'2010-02-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (85, 7, 2, CAST(N'2009-12-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (86, 7, 2, CAST(N'2009-01-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (87, 7, 3, CAST(N'2008-12-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (88, 7, 3, CAST(N'2009-12-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (89, 7, 3, CAST(N'2010-02-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (90, 7, 3, CAST(N'2010-02-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (91, 7, 3, CAST(N'2010-01-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (92, 7, 3, CAST(N'2010-03-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (93, 7, 2, CAST(N'2008-12-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (94, 7, 2, CAST(N'2008-12-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (95, 7, 2, CAST(N'2008-12-19' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (96, 7, 2, CAST(N'2009-03-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (97, 7, 2, CAST(N'2009-02-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (98, 7, 2, CAST(N'2009-02-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (99, 7, 2, CAST(N'2009-01-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (100, 7, 2, CAST(N'2009-01-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (101, 7, 2, CAST(N'2009-01-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (102, 7, 2, CAST(N'2008-12-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (103, 7, 2, CAST(N'2009-12-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (104, 7, 2, CAST(N'2009-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (105, 7, 2, CAST(N'2009-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (106, 7, 2, CAST(N'2010-02-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (107, 7, 2, CAST(N'2010-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (108, 7, 1, CAST(N'2008-12-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (109, 7, 1, CAST(N'2008-12-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (110, 7, 1, CAST(N'2008-12-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (111, 7, 1, CAST(N'2008-12-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (112, 7, 1, CAST(N'2009-01-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (113, 7, 1, CAST(N'2008-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (114, 7, 1, CAST(N'2009-01-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (115, 7, 1, CAST(N'2009-01-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (116, 7, 1, CAST(N'2009-01-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (117, 7, 1, CAST(N'2009-02-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (118, 7, 1, CAST(N'2009-02-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (119, 7, 1, CAST(N'2009-02-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (120, 7, 1, CAST(N'2009-03-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (121, 15, 1, CAST(N'2009-01-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (122, 15, 2, CAST(N'2008-12-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (123, 15, 2, CAST(N'2008-12-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (124, 15, 3, CAST(N'2008-12-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (125, 15, 1, CAST(N'2009-01-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (126, 15, 1, CAST(N'2009-02-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (127, 7, 3, CAST(N'2009-01-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (128, 7, 3, CAST(N'2008-12-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (129, 7, 3, CAST(N'2008-12-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (130, 7, 3, CAST(N'2009-02-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (131, 7, 3, CAST(N'2009-02-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (132, 7, 3, CAST(N'2009-02-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (133, 7, 3, CAST(N'2009-01-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (134, 7, 1, CAST(N'2009-01-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (135, 7, 1, CAST(N'2008-12-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (136, 7, 1, CAST(N'2009-12-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (137, 7, 1, CAST(N'2009-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (138, 7, 1, CAST(N'2009-02-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (139, 7, 1, CAST(N'2010-01-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (140, 7, 1, CAST(N'2010-01-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (141, 7, 1, CAST(N'2010-01-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (142, 7, 1, CAST(N'2010-03-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (143, 7, 1, CAST(N'2010-02-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (144, 7, 1, CAST(N'2009-01-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (145, 7, 2, CAST(N'2009-01-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (146, 7, 2, CAST(N'2008-12-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (147, 7, 2, CAST(N'2008-12-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (148, 7, 2, CAST(N'2009-01-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (149, 7, 2, CAST(N'2009-01-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (150, 7, 2, CAST(N'2009-02-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (151, 7, 2, CAST(N'2009-02-19' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (152, 7, 2, CAST(N'2009-02-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (153, 7, 2, CAST(N'2008-12-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (154, 7, 2, CAST(N'2008-12-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (155, 7, 2, CAST(N'2009-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (156, 7, 2, CAST(N'2009-01-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (157, 7, 2, CAST(N'2009-02-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (158, 7, 2, CAST(N'2009-03-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (159, 7, 2, CAST(N'2009-02-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (160, 7, 1, CAST(N'2009-01-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (161, 7, 1, CAST(N'2008-12-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (162, 7, 1, CAST(N'2009-01-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (163, 7, 1, CAST(N'2009-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (164, 7, 1, CAST(N'2009-03-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (165, 7, 1, CAST(N'2009-01-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (166, 7, 1, CAST(N'2009-02-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (167, 7, 1, CAST(N'2008-12-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (168, 7, 1, CAST(N'2008-12-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (169, 7, 1, CAST(N'2008-12-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (170, 7, 1, CAST(N'2008-12-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (171, 7, 1, CAST(N'2009-01-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (172, 7, 1, CAST(N'2009-01-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (173, 7, 1, CAST(N'2009-01-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (174, 7, 1, CAST(N'2009-01-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (175, 7, 1, CAST(N'2009-02-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (176, 7, 1, CAST(N'2009-02-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (177, 7, 1, CAST(N'2009-02-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (178, 7, 1, CAST(N'2009-03-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (179, 7, 1, CAST(N'2009-03-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (180, 7, 3, CAST(N'2009-02-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (181, 7, 3, CAST(N'2008-12-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (182, 7, 3, CAST(N'2008-12-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (183, 7, 3, CAST(N'2009-01-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (184, 7, 3, CAST(N'2009-02-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (185, 7, 3, CAST(N'2009-02-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (186, 7, 2, CAST(N'2009-02-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (187, 7, 2, CAST(N'2008-12-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (188, 7, 2, CAST(N'2008-12-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (189, 7, 2, CAST(N'2009-01-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (190, 7, 2, CAST(N'2009-02-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (191, 7, 2, CAST(N'2009-02-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (192, 7, 1, CAST(N'2009-03-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (193, 7, 1, CAST(N'2008-12-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (194, 7, 1, CAST(N'2008-12-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (195, 7, 1, CAST(N'2008-12-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (196, 7, 1, CAST(N'2008-12-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (197, 7, 1, CAST(N'2008-12-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (198, 7, 1, CAST(N'2009-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (199, 7, 1, CAST(N'2009-01-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (200, 7, 1, CAST(N'2009-01-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (201, 7, 1, CAST(N'2009-01-29' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (202, 7, 1, CAST(N'2009-02-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (203, 7, 1, CAST(N'2009-02-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (204, 7, 1, CAST(N'2009-03-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (205, 7, 3, CAST(N'2009-02-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (206, 7, 3, CAST(N'2008-12-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (207, 7, 3, CAST(N'2008-12-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (208, 7, 3, CAST(N'2009-01-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (209, 7, 3, CAST(N'2009-01-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (210, 7, 3, CAST(N'2009-02-20' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (211, 13, 1, CAST(N'2009-02-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (212, 13, 1, CAST(N'2008-12-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (213, 13, 3, CAST(N'2010-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (214, 13, 2, CAST(N'2009-02-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (215, 13, 1, CAST(N'2009-01-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (216, 13, 1, CAST(N'2008-12-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (217, 12, 1, CAST(N'2009-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (218, 12, 3, CAST(N'2008-12-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (219, 12, 1, CAST(N'2009-01-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (220, 12, 1, CAST(N'2009-02-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (221, 12, 2, CAST(N'2009-03-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (222, 8, 1, CAST(N'2008-12-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (223, 8, 1, CAST(N'2009-01-26' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (224, 7, 1, CAST(N'2009-01-07' AS Date), CAST(N'2011-08-31' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (224, 8, 1, CAST(N'2011-09-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (225, 8, 2, CAST(N'2009-02-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (226, 8, 3, CAST(N'2009-03-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (227, 14, 1, CAST(N'2009-12-02' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (228, 14, 1, CAST(N'2008-12-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (229, 14, 3, CAST(N'2010-02-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (230, 14, 2, CAST(N'2010-03-05' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (231, 14, 3, CAST(N'2010-03-07' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (232, 14, 2, CAST(N'2010-01-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (233, 14, 1, CAST(N'2009-12-21' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (234, 10, 1, CAST(N'2009-01-31' AS Date), CAST(N'2013-11-13' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (234, 16, 1, CAST(N'2013-11-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (235, 9, 1, CAST(N'2008-12-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (236, 9, 1, CAST(N'2009-02-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (237, 9, 1, CAST(N'2009-02-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (238, 9, 1, CAST(N'2009-01-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (239, 9, 1, CAST(N'2008-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (240, 9, 1, CAST(N'2008-12-13' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (241, 10, 1, CAST(N'2009-01-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (242, 10, 1, CAST(N'2008-12-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (243, 10, 1, CAST(N'2009-01-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (244, 10, 1, CAST(N'2009-01-24' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (245, 10, 1, CAST(N'2009-02-18' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (246, 10, 1, CAST(N'2009-02-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (247, 10, 1, CAST(N'2009-03-01' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (248, 10, 1, CAST(N'2009-03-08' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (249, 10, 1, CAST(N'2008-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (250, 4, 1, CAST(N'2011-02-25' AS Date), CAST(N'2011-07-30' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (250, 13, 1, CAST(N'2011-07-31' AS Date), CAST(N'2012-07-14' AS Date))
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (250, 5, 1, CAST(N'2012-07-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (251, 5, 1, CAST(N'2009-02-10' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (252, 5, 1, CAST(N'2009-02-28' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (253, 5, 1, CAST(N'2009-12-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (254, 5, 1, CAST(N'2010-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (255, 5, 1, CAST(N'2010-01-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (256, 5, 1, CAST(N'2010-01-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (257, 5, 1, CAST(N'2010-01-27' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (258, 5, 1, CAST(N'2010-01-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (259, 5, 1, CAST(N'2010-03-09' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (260, 5, 1, CAST(N'2010-12-06' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (261, 5, 1, CAST(N'2010-12-25' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (262, 10, 1, CAST(N'2009-01-12' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (263, 11, 1, CAST(N'2008-12-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (264, 11, 1, CAST(N'2009-02-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (265, 11, 2, CAST(N'2008-12-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (266, 11, 1, CAST(N'2009-02-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (267, 11, 1, CAST(N'2009-02-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (268, 11, 1, CAST(N'2009-02-03' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (269, 11, 1, CAST(N'2009-01-11' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (270, 11, 1, CAST(N'2009-01-17' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (271, 11, 1, CAST(N'2009-01-22' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (272, 11, 1, CAST(N'2008-12-23' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (273, 3, 1, CAST(N'2011-02-15' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (274, 3, 1, CAST(N'2011-01-04' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (275, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (276, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (277, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (278, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (279, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (280, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (281, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (282, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (283, 3, 1, CAST(N'2011-05-31' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (284, 3, 1, CAST(N'2012-09-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (285, 3, 1, CAST(N'2013-03-14' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (286, 3, 1, CAST(N'2013-05-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (287, 3, 1, CAST(N'2012-04-16' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (288, 3, 1, CAST(N'2013-05-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (289, 3, 1, CAST(N'2012-05-30' AS Date), NULL)
    
    INSERT [dbo].[EmployeeDepartmentHistory] ([EmployeeID], [DepartmentID], [ShiftID], [StartDate], [EndDate]) VALUES (290, 3, 1, CAST(N'2012-05-30' AS Date), NULL)
END