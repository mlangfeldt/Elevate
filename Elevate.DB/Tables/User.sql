﻿Create Table [dbo].[User](
	[Id] INT Not Null Primary Key,
	[Email] VARCHAR(50) Not Null,
	[Password] VARCHAR(50)  Not Null,
	[FirstName] VARCHAR(50)  Not Null,
	[LastName] VARCHAR(50)  Not Null,
)