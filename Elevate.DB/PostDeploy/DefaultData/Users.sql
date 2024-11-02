BEGIN
	INSERT INTO tblUser (Id,Email,Password,FirstName,LastName,ResetCode,ResetCodeExpiration,EmailConfirmed,ConfirmationCode)
	VALUES
	(1,'Joe','Smith','joesmith123','123',null,null,0,null),
	(2,'Susan','Smith','susansmith123','123',null,null,0,null),
	(3,'Steven','Smith','stevensmith123','123',null,null,0,null)
	
END