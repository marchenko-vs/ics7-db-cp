IF OBJECT_ID('[Tickets]', 'U') IS NULL
CREATE TABLE [Tickets] (
	[Id] [bigint] IDENTITY(1, 1),
	[FlightId] [bigint] NOT NULL,
	[Available] [bit] NOT NULL,
	[Row] [int] NOT NULL,
	[Place] [char] NOT NULL,
	[Class] [nvarchar](16) NOT NULL,
	[Refund] [bit] NOT NULL,
	[Price] [money]
	PRIMARY KEY (Id),
	CONSTRAINT FK_FLIGHTS_2_ID
	FOREIGN KEY (FlightId) REFERENCES [Flights] (Id)
	ON DELETE CASCADE
);
