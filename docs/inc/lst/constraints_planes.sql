IF OBJECT_ID('[Planes]', 'U') IS NULL
CREATE TABLE [Planes] (
	[Id] [bigint] IDENTITY(1, 1),
	[Manufacturer] [nvarchar](32) NOT NULL,
	[Model] [nvarchar](16) NOT NULL,
	[EconomyClassNum] [integer] NOT NULL,
	[FirstClassNum] [integer] NOT NULL,
	[BusinessClassNum] [integer] NOT NULL,
	PRIMARY KEY (Id),
);
