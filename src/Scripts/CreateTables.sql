IF OBJECT_ID('[Users]', 'U') IS NULL
CREATE TABLE [Users] (
	[Id] [bigint] IDENTITY(1, 1),
	[Role] [varchar](32) NOT NULL,
	[Email] [varchar](32) NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[FirstName] [nvarchar](32),
	[LastName] [nvarchar](32),
	[RegDate] [date] NOT NULL,
	PRIMARY KEY (Id),
);
GO

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
GO

IF OBJECT_ID('[Flights]', 'U') IS NULL
CREATE TABLE [Flights] (
	[Id] [bigint] IDENTITY(1, 1),
	[DeparturePoint] [nvarchar](32) NOT NULL,
	[ArrivalPoint] [nvarchar](32) NOT NULL,
	[DepartureDateTime] [datetime] NOT NULL,
	[ArrivalDateTime] [datetime] NOT NULL,
	PRIMARY KEY (Id),
);
GO

IF OBJECT_ID('[PlanesFlights]', 'U') IS NULL
CREATE TABLE [PlanesFlights] (
	[PlaneId] [bigint],
	[FlightId] [bigint],
	PRIMARY KEY (PlaneId, FlightId),
	CONSTRAINT FK_PLANES_ID
	FOREIGN KEY (PlaneId) REFERENCES [Planes] (Id)
	ON DELETE CASCADE,
	CONSTRAINT FK_FLIGHTS_ID
	FOREIGN KEY (FlightId) REFERENCES [Flights] (Id)
	ON DELETE CASCADE
);
GO

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
GO

IF OBJECT_ID('[Orders]', 'U') IS NULL
CREATE TABLE [Orders] (
	[Id] [bigint] IDENTITY(1, 1),
	[UserId] [bigint],
	[Status] [nvarchar](16),
	PRIMARY KEY (Id),
	CONSTRAINT FK_USERS_ID
	FOREIGN KEY (UserId) REFERENCES [Users] (Id)
	ON DELETE CASCADE
);
GO

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
GO

IF OBJECT_ID('[OrdersTickets]', 'U') IS NULL
CREATE TABLE [OrdersTickets] (
	[OrderId] [bigint],
	[TicketId] [bigint],
	PRIMARY KEY (OrderId, TicketId),
	CONSTRAINT FK_ORDERS_ID
	FOREIGN KEY (OrderId) REFERENCES [Orders] (Id)
	ON DELETE CASCADE,
	CONSTRAINT FK_TICKETS_ID
	FOREIGN KEY (TicketId) REFERENCES [Tickets] (Id)
	ON DELETE CASCADE
);
GO

IF OBJECT_ID('[TicketsServices]', 'U') IS NULL
CREATE TABLE [TicketsServices] (
	[TicketId] [bigint],
	[ServiceId] [bigint],
	PRIMARY KEY (TicketId, ServiceId),
	CONSTRAINT FK_TICKETS_2_ID
	FOREIGN KEY (TicketId) REFERENCES [Tickets] (Id)
	ON DELETE CASCADE,
	CONSTRAINT FK_SERVICES_ID
	FOREIGN KEY (ServiceId) REFERENCES [Services] (Id)
	ON DELETE CASCADE
);
GO
