IF NOT EXISTS (SELECT TOP 1 1 FROM ShipMethod)
BEGIN
	PRINT CONVERT(varchar(20), GETDATE(), 113) + ' Populating table ShipMethod...'

	INSERT INTO ShipMethod (Name, ShipBase, ShipRate) VALUES ('XRQ - TRUCK GROUND', 3.95, 0.99)
	INSERT INTO ShipMethod (Name, ShipBase, ShipRate) VALUES ('ZY - EXPRESS', 9.95, 1.99)
	INSERT INTO ShipMethod (Name, ShipBase, ShipRate) VALUES ('OVERSEAS - DELUXE', 29.95, 2.99)
	INSERT INTO ShipMethod (Name, ShipBase, ShipRate) VALUES ('OVERNIGHT J-FAST', 21.95, 1.29)
	INSERT INTO ShipMethod (Name, ShipBase, ShipRate) VALUES ('CARGO TRANSPORT 5', 8.99, 1.49)
END
GO