IF NOT EXISTS (SELECT TOP 1 1 FROM ProductCategory)
BEGIN
    PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table ProductCategory...'

	INSERT INTO ProductCategory (Name) VALUES ('Bikes')
	INSERT INTO ProductCategory (Name) VALUES ('Components')
    INSERT INTO ProductCategory (Name) VALUES ('Clothing')
    INSERT INTO ProductCategory (Name) VALUES ('Accessories')
END
GO