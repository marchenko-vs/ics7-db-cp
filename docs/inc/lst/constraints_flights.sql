IF OBJECT_ID('[Flights]', 'U') IS NULL
CREATE TABLE [Flights] (
	[Id] [bigint] IDENTITY(1, 1),
	[DeparturePoint] [nvarchar](32) NOT NULL,
	[ArrivalPoint] [nvarchar](32) NOT NULL,
	[DepartureDate] [date] NOT NULL,
	[ArrivalDate] [date] NOT NULL,
	[DepartureTime] [time] NOT NULL,
	[ArrivalTime] [time] NOT NULL,
	PRIMARY KEY (Id),
);
