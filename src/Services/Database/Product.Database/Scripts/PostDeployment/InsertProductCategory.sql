IF NOT EXISTS (SELECT TOP 1 1 FROM ProductCategory)
BEGIN
	INSERT INTO ProductCategory (Name, rowguid, ModifiedDate) VALUES ('Bikes', 'CFBDA25C-DF71-47A7-B81B-64EE161AA37C', GETDATE())
	INSERT INTO ProductCategory (Name, rowguid, ModifiedDate) VALUES ('Components', 'C657828D-D808-4ABA-91A3-AF2CE02300E9', GETDATE())
    INSERT INTO ProductCategory (Name, rowguid, ModifiedDate) VALUES ('Clothing', '10A7C342-CA82-48D4-8A38-46A2EB089B74', GETDATE())
    INSERT INTO ProductCategory (Name, rowguid, ModifiedDate) VALUES ('Accessories', '2BE3BE36-D9A2-4EEE-B593-ED895D97C2A6', GETDATE())
END