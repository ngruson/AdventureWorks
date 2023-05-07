CREATE TABLE [dbo].[EmployeeDepartmentHistory](
    [EmployeeDepartmentHistoryID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[EmployeeID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[ShiftID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL
    CONSTRAINT [PK_EmployeeDepartmentHistoryID] PRIMARY KEY CLUSTERED 
    (
	    [EmployeeDepartmentHistoryID]
    ),
    CONSTRAINT [FK_EmployeeDepartmentHistory_Employee_EmployeeID] FOREIGN KEY([EmployeeID])
        REFERENCES [Employee] ([PersonID]),
    CONSTRAINT [FK_EmployeeDepartmentHistory_Department_DepartmentID] FOREIGN KEY([DepartmentID])
        REFERENCES [Department] ([DepartmentID]),
    CONSTRAINT [FK_EmployeeDepartmentHistory_Shift_ShiftID] FOREIGN KEY([ShiftID])
        REFERENCES [Shift] ([ShiftID]),
    CONSTRAINT UC_EmployeeDepartmentHistory_ObjectID UNIQUE(ObjectID),
    CONSTRAINT [CK_EmployeeDepartmentHistory_EndDate] CHECK  (([EndDate]>=[StartDate] OR [EndDate] IS NULL))
)