INSERT INTO ClientSecrets (ClientId, Description, Value, Expiration, Type, Created)
SELECT Id, NULL, 'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', NULL, 'SharedSecret', GETDATE()
FROM Clients
ORDER BY Id