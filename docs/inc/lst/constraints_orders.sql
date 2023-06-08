IF OBJECT_ID('[Orders]', 'U') IS NULL
CREATE TABLE [Orders] (
	[Id] [bigint] IDENTITY(1, 1),
	[UserId] [bigint],
	[Status] [nvarchar](16),
	PRIMARY KEY (Id),
	CONSTRAINT FK_USERS_ID
	FOREIGN KEY (UserId) REFERENCES [Users] (Id)
	ON DELETE CASCADE
);
