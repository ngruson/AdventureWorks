IF NOT EXISTS (SELECT TOP 1 1 FROM Shift)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table Shift...'

	SET IDENTITY_INSERT [Shift] ON

    INSERT [Shift] ([ShiftID],[Name],[StartTime],[EndTime])
    SELECT 1, N'Day','07:00:00', '15:00:00' UNION ALL
    SELECT 2, N'Evening','15:00:00','23:00:00' UNION ALL
    SELECT 3, N'Night','23:00:00','07:00:00';

	SET IDENTITY_INSERT [Shift] OFF
END
GO