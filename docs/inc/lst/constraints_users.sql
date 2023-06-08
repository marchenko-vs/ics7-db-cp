IF OBJECT_ID('[Users]', 'U') IS NULL
CREATE TABLE [Users] (
	[Id] [bigint] IDENTITY(1, 1),
	[Role] [varchar](32) NOT NULL,
	[Email] [varchar](32) NOT NULL UNIQUE,
	[Password] [varchar](64) NOT NULL,
	[FirstName] [nvarchar](32),
	[LastName] [nvarchar](32),
	[RegDate] [date] NOT NULL,
	PRIMARY KEY (Id),
);
