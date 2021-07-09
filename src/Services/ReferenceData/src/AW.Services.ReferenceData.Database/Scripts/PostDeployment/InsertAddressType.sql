IF NOT EXISTS (SELECT TOP 1 1 FROM AddressType)
BEGIN
	INSERT INTO AddressType (Name) VALUES ('Billing')
	INSERT INTO AddressType (Name) VALUES ('Home')
	INSERT INTO AddressType (Name) VALUES ('Main Office')
	INSERT INTO AddressType (Name) VALUES ('Primary')
	INSERT INTO AddressType (Name) VALUES ('Shipping')
	INSERT INTO AddressType (Name) VALUES ('Archive')
END