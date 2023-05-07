CREATE TABLE [dbo].[Shift](
	[ShiftID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] nvarchar(50) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL
    CONSTRAINT PK_Shift_ShiftID PRIMARY KEY CLUSTERED 
    (
	    ShiftID
    ),
    CONSTRAINT UC_Shift_ObjectID UNIQUE(ObjectID)
)