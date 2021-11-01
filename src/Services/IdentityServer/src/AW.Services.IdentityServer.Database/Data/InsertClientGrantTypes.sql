INSERT INTO ClientGrantTypes (GrantType, ClientId)
SELECT 'password', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientGrantTypes (GrantType, ClientId)
SELECT 'client_credentials', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientGrantTypes (GrantType, ClientId)
SELECT 'authorization_code', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientGrantTypes (GrantType, ClientId)
SELECT 'authorization_code', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientGrantTypes (GrantType, ClientId)
SELECT 'authorization_code', Id
FROM Clients
WHERE ClientId = 'swagger'