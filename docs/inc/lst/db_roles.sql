CREATE ROLE db_customer;
CREATE ROLE db_moderator;
CREATE ROLE db_admin;
GRANT EXECUTE ON GetOrderPrice TO db_customer, db_moderator, db_admin;
GRANT SELECT ON Flights TO db_customer, db_moderator, db_admin;
GRANT SELECT, UPDATE, INSERT ON Orders TO db_customer, db_moderator, db_admin;
GRANT SELECT ON Planes TO db_customer, db_moderator, db_admin;
GRANT SELECT ON Services TO db_customer, db_moderator, db_admin;
GRANT SELECT, UPDATE ON Tickets TO db_customer, db_moderator, db_admin;
GRANT SELECT, UPDATE, INSERT, DELETE ON TicketsServices TO db_customer, db_moderator, db_admin;
GRANT SELECT, UPDATE, INSERT ON Users TO db_customer, db_moderator, db_admin;
GRANT UPDATE, INSERT, DELETE ON Flights TO db_moderator, db_admin;
GRANT UPDATE, INSERT, DELETE ON Planes TO db_moderator, db_admin;
GRANT INSERT, DELETE ON Tickets TO db_moderator, db_admin;
GRANT DELETE ON Orders TO db_admin;
GRANT UPDATE, INSERT, DELETE ON Services TO db_admin;
GRANT DELETE ON Users TO db_admin;
DENY CREATE TABLE TO db_customer, db_moderator;
GRANT CREATE TABLE TO db_admin;
GRANT ALTER ANY SCHEMA TO db_admin;
DENY DELETE ON OBJECT::Users TO db_customer, db_moderator;
DENY DELETE ON OBJECT::Orders TO db_customer, db_moderator;
DENY DELETE ON OBJECT::Tickets TO db_customer, db_moderator;
DENY DELETE ON OBJECT::Flights TO db_customer, db_moderator;
DENY DELETE ON OBJECT::Planes TO db_customer, db_moderator;
DENY DELETE ON OBJECT::Services TO db_customer, db_moderator;
DENY DELETE ON OBJECT::TicketsServices TO db_customer, db_moderator;
GRANT DELETE ON OBJECT::Users TO db_admin;
GRANT DELETE ON OBJECT::Orders TO db_admin;
GRANT DELETE ON OBJECT::Tickets TO db_admin;
GRANT DELETE ON OBJECT::Flights TO db_admin;
GRANT DELETE ON OBJECT::Planes TO db_admin;
GRANT DELETE ON OBJECT::Services TO db_admin;
GRANT DELETE ON OBJECT::TicketsServices TO db_admin;
CREATE LOGIN customer WITH PASSWORD = 'customer';
CREATE USER customer FOR LOGIN customer;
CREATE LOGIN moderator WITH PASSWORD = 'moderator';
CREATE USER moderator FOR LOGIN moderator;
CREATE LOGIN admin WITH PASSWORD = 'admin';
CREATE USER admin FOR LOGIN admin;
ALTER ROLE db_customer ADD MEMBER customer;
ALTER ROLE db_moderator ADD MEMBER moderator;
ALTER ROLE db_admin ADD MEMBER admin;
