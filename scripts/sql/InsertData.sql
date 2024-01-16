BULK INSERT [dbo].[Flights]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\flights.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Tickets]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\tickets.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);

BULK INSERT [dbo].[Planes]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\planes.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
GO

BULK INSERT [dbo].[Services]
FROM 'C:\Users\Vladyslav\source\repos\ics7-db-cp\src\Scripts\Data\services.csv'
WITH (
  FIELDTERMINATOR = ',',
  ROWTERMINATOR = '\n',
  FIRSTROW = 2,
  datafiletype = 'widechar'
);
