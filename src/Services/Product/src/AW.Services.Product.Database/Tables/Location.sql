CREATE TABLE [Location](
	[LocationID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[CostRate] [smallmoney] NOT NULL,
	[Availability] [decimal](8, 2) NOT NULL
     CONSTRAINT [PK_Location_LocationID] PRIMARY KEY CLUSTERED
    (
    	[LocationID] ASC
    )
)