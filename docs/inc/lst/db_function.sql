CREATE FUNCTION GetOrderPrice (@OrderId bigint)
    RETURNS money
    BEGIN
        DECLARE @sumTickets money, @sumServices money, @result money
	SET @sumTickets = 0;
	SET @sumServices = 0;
	SET @result = 0;
        SELECT @sumTickets = COALESCE(SUM(t.Price), 0) FROM Orders o 
		JOIN OrdersTickets ot ON o.Id = ot.OrderId JOIN
		Tickets t ON ot.TicketId = t.Id WHERE @OrderId = o.Id;
	SELECT @sumServices = COALESCE(SUM(s.Price), 0) FROM Orders o 
		JOIN OrdersTickets ot ON o.Id = ot.OrderId JOIN
		TicketsServices ts ON ot.TicketId = ts.TicketId 
		JOIN Services s ON s.Id = ts.ServiceId WHERE @OrderId = o.Id;
		SELECT @result = @sumTickets + @sumServices
        RETURN @result
    END;
