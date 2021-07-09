IF NOT EXISTS (SELECT TOP 1 1 FROM ProductCategory)
BEGIN
	INSERT INTO ProductCategory (Name) VALUES ('Bikes')
	INSERT INTO ProductCategory (Name) VALUES ('Components')
    INSERT INTO ProductCategory (Name) VALUES ('Clothing')
    INSERT INTO ProductCategory (Name) VALUES ('Accessories')
END