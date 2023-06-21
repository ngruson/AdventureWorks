CREATE TABLE [dbo].[ShipMethod]
(
	[ShipMethodID] [int] IDENTITY(1,1) NOT NULL,
    [ObjectID] uniqueidentifier NOT NULL DEFAULT NEWID(),
	[Name] nvarchar(50) NOT NULL,
	[ShipBase] [money] NOT NULL,
	[ShipRate] [money] NOT NULL
    CONSTRAINT [PK_ShipMethod_ShipMethodID] PRIMARY KEY CLUSTERED ( [ShipMethodID] ASC ),
    CONSTRAINT UC_ShipMethod_ObjectID UNIQUE(ObjectID)
)