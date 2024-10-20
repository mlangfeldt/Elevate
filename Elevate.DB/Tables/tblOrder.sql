CREATE TABLE [dbo].[tblOrder] (
	[Id] int not null primary key,
	[CustomerId] int not null,
	[OrderDate] DateTime not null,
	[UserId] int not null, 

)