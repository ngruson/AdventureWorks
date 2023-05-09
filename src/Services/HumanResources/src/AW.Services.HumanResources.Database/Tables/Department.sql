CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] nvarchar(50) NOT NULL,
	[GroupName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Department_DepartmentID] PRIMARY KEY CLUSTERED 
    (
	    [DepartmentID] ASC
    ),
    CONSTRAINT UC_Department_ObjectID UNIQUE(ObjectID)
)