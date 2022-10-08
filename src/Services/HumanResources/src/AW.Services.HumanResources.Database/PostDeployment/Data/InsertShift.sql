IF NOT EXISTS (SELECT TOP 1 1 FROM Shift)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Shift...'

	SET IDENTITY_INSERT [Shift] ON

	INSERT [Shift] ([ShiftID], [Name], [StartTime], [EndTime], [ModifiedDate]) VALUES (1, N'Day', CAST(N'07:00:00' AS Time), CAST(N'15:00:00' AS Time), GETDATE())
	
	INSERT [Shift] ([ShiftID], [Name], [StartTime], [EndTime], [ModifiedDate]) VALUES (2, N'Evening', CAST(N'15:00:00' AS Time), CAST(N'23:00:00' AS Time), GETDATE())
	
	INSERT [Shift] ([ShiftID], [Name], [StartTime], [EndTime], [ModifiedDate]) VALUES (3, N'Night', CAST(N'23:00:00' AS Time), CAST(N'07:00:00' AS Time), GETDATE())

	SET IDENTITY_INSERT [Shift] OFF
END
GO