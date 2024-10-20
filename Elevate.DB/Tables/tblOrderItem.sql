CREATE TABLE [dbo].[tblOrderItem](
	[Id] int not null primary key,
	[OrderId] int not null,
	[CourseId] int not null,
	[Quantity] int not null,
	[Cost] float not null,
)