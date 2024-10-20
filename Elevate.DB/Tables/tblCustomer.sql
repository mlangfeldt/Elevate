CREATE TABLE [dbo].[tblCustomer](
	[Id] int not null primary key,
	[UserId] int not null,
	[FirstName] varchar(50) not null,
	[LastName] varchar(50) not null,
	[Email] varchar(35) not null
)