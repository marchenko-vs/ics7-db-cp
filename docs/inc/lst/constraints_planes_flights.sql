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
