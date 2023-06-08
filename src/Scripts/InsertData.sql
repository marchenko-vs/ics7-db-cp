BULK INSERT [dbo].[Planes]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Planes.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
GO

BULK INSERT [dbo].[Flights]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Flights.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Tickets]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Tickets.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Services]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Services.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

SELECT * FROM [dbo].[Users];
SELECT * FROM [dbo].[Planes];
SELECT * FROM [dbo].[Flights];
SELECT * FROM [dbo].[Tickets];
SELECT * FROM [dbo].[Orders];
SELECT * FROM [dbo].[Services];
SELECT * FROM [dbo].[OrdersTickets];
SELECT * FROM [dbo].[TicketsServices];
SELECT * FROM [dbo].[PlanesFlights];
