CREATE TABLE [dbo].[tblUser]
(
	[Id] INT Not Null Primary Key,
	[Email] VARCHAR(50) Not Null,
	[Password] VARCHAR(50)  Not Null,
	[FirstName] VARCHAR(50)  Not Null,
	[LastName] VARCHAR(50)  Not Null,
	[ResetCode] VARCHAR(6),
	[ResetCodeExpiration] DateTime, 
    [EmailConfirmed] INT NOT NULL, 
    [ConfirmationCode] VARCHAR(32) NULL
)
