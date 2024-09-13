Create Table [dbo].[Collection](
	[Id] UniqueIdentifier Not Null Primary Key,
	[CourseId] UniqueIdentifier  Not Null,
	[UserId] UniqueIdentifier Not Null
)