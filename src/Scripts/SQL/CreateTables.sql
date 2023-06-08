IF OBJECT_ID('[Users]', 'U') IS NULL
CREATE TABLE [Users] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[Role] [varchar](32) NOT NULL,
	[Email] [varchar](32) NOT NULL,
	[Password] [varchar](64) NOT NULL,
	[FirstName] [nvarchar](32),
	[LastName] [nvarchar](32),
	[RegDate] [datetime] NOT NULL,
	PRIMARY KEY (Id),
);
GO

IF OBJECT_ID('[Planes]', 'U') IS NULL
CREATE TABLE [Planes] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[Manufacturer] [nvarchar](32) NOT NULL,
	[Model] [nvarchar](16) NOT NULL,
	[EconomyClassNum] [integer] NOT NULL CHECK (EconomyClassNum >= 0),
	[BusinessClassNum] [integer] NOT NULL CHECK (BusinessClassNum >= 0),
	[FirstClassNum] [integer] NOT NULL CHECK (FirstClassNum >= 0),
	PRIMARY KEY (Id),
);
GO

IF OBJECT_ID('[Flights]', 'U') IS NULL
CREATE TABLE [Flights] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[PlaneId] [bigint] NOT NULL CHECK (PlaneId > 0),
	[DeparturePoint] [nvarchar](32) NOT NULL,
	[ArrivalPoint] [nvarchar](32) NOT NULL,
	[DepartureDateTime] [datetime] NOT NULL,
	[ArrivalDateTime] [datetime] NOT NULL,
	PRIMARY KEY (Id),
	CHECK (ArrivalDateTime > DepartureDateTime),
	CONSTRAINT FK_PLANES_ID
	FOREIGN KEY (PlaneId) REFERENCES [Planes] (Id)
	ON DELETE CASCADE
);
GO

IF OBJECT_ID('[Orders]', 'U') IS NULL
CREATE TABLE [Orders] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[UserId] [bigint] CHECK (UserId > 0),
	[Status] [nvarchar](16) NOT NULL,
	PRIMARY KEY (Id),
	CONSTRAINT FK_USERS_ID
	FOREIGN KEY (UserId) REFERENCES [Users] (Id)
	ON DELETE CASCADE,
);
GO

IF OBJECT_ID('[Tickets]', 'U') IS NULL
CREATE TABLE [Tickets] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[FlightId] [bigint] NOT NULL CHECK (FlightId > 0),
	[OrderId] [bigint],
	[Row] [int] NOT NULL CHECK (Row > 0),
	[Place] [char] NOT NULL CHECK (Place > 'A' AND Place < 'L'),
	[Class] [nvarchar](16) NOT NULL,
	[Refund] [bit] NOT NULL,
	[Price] [money] CHECK (Price > 0),
	PRIMARY KEY (Id),
	CONSTRAINT FK_FLIGHTS_ID
	FOREIGN KEY (FlightId) REFERENCES [Flights] (Id)
	ON DELETE CASCADE,
	CONSTRAINT FK_ORDERS_ID
	FOREIGN KEY (OrderId) REFERENCES [Orders] (Id)
	ON DELETE CASCADE,
);
GO

IF OBJECT_ID('[Services]', 'U') IS NULL
CREATE TABLE [Services] (
	[Id] [bigint] IDENTITY(1, 1) CHECK (Id > 0),
	[Name] [nvarchar](64) NOT NULL,
	[Price] [money] NOT NULL CHECK (Price > 0),
	[EconomyClass] [bit] NOT NULL,
	[BusinessClass] [bit] NOT NULL,
	[FirstClass] [bit] NOT NULL,
	PRIMARY KEY (Id),
);
GO

IF OBJECT_ID('[TicketsServices]', 'U') IS NULL
CREATE TABLE [TicketsServices] (
	[TicketId] [bigint] CHECK (TicketId > 0),
	[ServiceId] [bigint] CHECK (ServiceId > 0),
	PRIMARY KEY (TicketId, ServiceId),
	CONSTRAINT FK_TICKETS_2_ID
	FOREIGN KEY (TicketId) REFERENCES [Tickets] (Id)
	ON DELETE CASCADE,
	CONSTRAINT FK_SERVICES_ID
	FOREIGN KEY (ServiceId) REFERENCES [Services] (Id)
	ON DELETE CASCADE
);
GO
