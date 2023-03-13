CREATE TABLE [Culture](
	[CultureID] [nvarchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
    CONSTRAINT [PK_Culture_CultureID] PRIMARY KEY CLUSTERED 
    (
	    [CultureID] ASC
    )
)