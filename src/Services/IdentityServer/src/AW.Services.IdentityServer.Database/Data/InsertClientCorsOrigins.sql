INSERT INTO ClientCorsOrigins (Origin, ClientId)
SELECT 'http://localhost:58093', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientCorsOrigins (Origin, ClientId)
SELECT 'https://localhost:44331', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientCorsOrigins (Origin, ClientId)
SELECT 'http://localhost:53154', Id
FROM Clients
WHERE ClientId = 'swagger'