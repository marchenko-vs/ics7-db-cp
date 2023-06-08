DROP LOGIN customer;
DROP LOGIN moderator;
DROP LOGIN admin;
GO

CREATE LOGIN customer WITH PASSWORD = 'customer';
CREATE USER customer FOR LOGIN customer;
CREATE ROLE db_customer;
GRANT EXECUTE ON GetOrderPrice TO db_customer;
GRANT SELECT ON Flights TO db_customer;
GRANT SELECT, UPDATE, INSERT ON Orders TO db_customer;
GRANT SELECT, UPDATE, INSERT, DELETE ON OrdersTickets TO db_customer;
GRANT SELECT ON Planes TO db_customer;
GRANT SELECT ON PlanesFlights TO db_customer;
GRANT SELECT ON Services TO db_customer;
GRANT SELECT, UPDATE ON Tickets TO db_customer;
GRANT SELECT, UPDATE, INSERT, DELETE ON TicketsServices TO db_customer;
GRANT SELECT, UPDATE, INSERT ON Users TO db_customer;
DENY CREATE TABLE TO Customer;
ALTER ROLE db_customer ADD MEMBER customer;
GO

CREATE LOGIN moderator WITH PASSWORD = 'moderator';
CREATE USER moderator FOR LOGIN moderator;
CREATE ROLE db_moderator;
GRANT EXECUTE ON GetOrderPrice TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON Flights TO db_moderator;
GRANT SELECT, UPDATE, INSERT ON Orders TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON OrdersTickets TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON Planes TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON PlanesFlights TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON Services TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON Tickets TO db_moderator;
GRANT SELECT, UPDATE, INSERT, DELETE ON TicketsServices TO db_moderator;
GRANT SELECT, UPDATE, INSERT ON Users TO db_moderator;
ALTER ROLE db_moderator ADD MEMBER moderator;
GO

CREATE LOGIN admin WITH PASSWORD = 'admin';
CREATE USER admin FOR LOGIN admin;
CREATE ROLE db_admin;
GRANT EXECUTE ON GetOrderPrice TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Flights TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Orders TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON OrdersTickets TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Planes TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON PlanesFlights TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Services TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Tickets TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON TicketsServices TO db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON Users TO db_admin;
ALTER ROLE db_admin ADD MEMBER admin;
GO
