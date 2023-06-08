IF OBJECT_ID('[Services]', 'U') IS NULL
CREATE TABLE [Services] (
	[Id] [bigint] IDENTITY(1, 1),
	[Name] [nvarchar](64) NOT NULL,
	[Price] [money] NOT NULL,
	[EconomyClass] [bit] NOT NULL,
	[FirstClass] [bit] NOT NULL,
	[BusinessClass] [bit] NOT NULL,
	PRIMARY KEY (Id),
);
