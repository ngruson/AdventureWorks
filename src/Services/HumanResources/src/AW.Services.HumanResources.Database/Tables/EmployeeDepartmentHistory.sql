CREATE TABLE [dbo].[EmployeeDepartmentHistory](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[ShiftID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[StartDate] ASC,
	[DepartmentID] ASC,
	[ShiftID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [EmployeeDepartmentHistory] ADD CONSTRAINT [FK_EmployeeDepartmentHistory_Department_DepartmentID] FOREIGN KEY([DepartmentID])
REFERENCES [Department] ([DepartmentID])
GO

ALTER TABLE [EmployeeDepartmentHistory] CHECK CONSTRAINT [FK_EmployeeDepartmentHistory_Department_DepartmentID]
GO

ALTER TABLE [EmployeeDepartmentHistory] ADD CONSTRAINT [FK_EmployeeDepartmentHistory_Employee_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [Employee] ([PersonID])
GO

ALTER TABLE [EmployeeDepartmentHistory] CHECK CONSTRAINT [FK_EmployeeDepartmentHistory_Employee_EmployeeID]
GO

ALTER TABLE [EmployeeDepartmentHistory] ADD CONSTRAINT [FK_EmployeeDepartmentHistory_Shift_ShiftID] FOREIGN KEY([ShiftID])
REFERENCES [Shift] ([ShiftID])
GO

ALTER TABLE [EmployeeDepartmentHistory] CHECK CONSTRAINT [FK_EmployeeDepartmentHistory_Shift_ShiftID]
GO

ALTER TABLE [EmployeeDepartmentHistory] ADD CONSTRAINT [CK_EmployeeDepartmentHistory_EndDate] CHECK  (([EndDate]>=[StartDate] OR [EndDate] IS NULL))
GO

ALTER TABLE [EmployeeDepartmentHistory] CHECK CONSTRAINT [CK_EmployeeDepartmentHistory_EndDate]
GO