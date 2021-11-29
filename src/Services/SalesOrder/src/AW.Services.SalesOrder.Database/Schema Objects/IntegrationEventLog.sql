CREATE TABLE [dbo].[IntegrationEventLog]
(
	[EventId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Content] NVARCHAR(MAX) NOT NULL,
	[CreationTime] DATETIME NOT NULL,
	[EventTypeName] NVARCHAR(100) NOT NULL,
	[State] INT NOT NULL,
	[TimesSent] INT NOT NULL,
	[TransactionId] NVARCHAR(50)
)