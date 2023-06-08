BULK INSERT [dbo].[Tickets]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\Tickets.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Flights]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\Flights.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Planes]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\Planes.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
GO

BULK INSERT [dbo].[Services]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Services.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
