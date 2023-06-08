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
