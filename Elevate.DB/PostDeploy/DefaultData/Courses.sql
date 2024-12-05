BEGIN
	INSERT INTO tblCourse (Id,Name,Description,Cost, ImgUrl)
	VALUES 
	(1,'Budget','This course teaches you the ins and outs of creating a budget.', 15.99, 'budget.jpg'),
	(2,'Debt Management','This course teaches you how to handle the debt you have.', 12.99, 'debt.jpg'),
	(3,'Investment','This course teaches you how to start investing in the stock market.', 18.99, 'invest.jpg'),
	(4,'Retirement','This course teaches you how to manage retirement.', 25.99, 'retire.jpg'),
	(5,'Debt Leverage','This course teaches you how to leverage debt.', 25.99, 'budget.jpg'),
	(6,'Day Trading','This course teaches you how to day trade.', 25.99, 'invest.jpg')
END