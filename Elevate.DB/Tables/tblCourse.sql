﻿CREATE TABLE [dbo].[tblCourse]
(
	[Id] INT Not Null Primary Key,
	[Name] VARCHAR(50) Not Null,
	[Description] VARCHAR(255) Not Null, 
    [Cost] FLOAT NOT NULL, 
    [ImgUrl] VARCHAR(50) NOT NULL
)
