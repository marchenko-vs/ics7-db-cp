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
