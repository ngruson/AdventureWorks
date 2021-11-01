/* Scopes for store MVC application */

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'openid', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'profile', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'email', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'offline_access', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'basket-api.read', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'basket-api.write', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'customer-api.read', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'product-api.read', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'salesorder-api.read', Id
FROM Clients
WHERE ClientId = 'store'

/* Scopes for internal MVC application */

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'openid', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'profile', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'email', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'basket-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'customer-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'product-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'referencedata-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'salesorder-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'salesperson-api.read', Id
FROM Clients
WHERE ClientId = 'internal'

/* Scopes for Swagger UI */

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'openid', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'profile', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'email', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'basket-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'basket-api.write', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'customer-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'product-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'referencedata-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'salesorder-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientScopes (Scope, ClientId)
SELECT 'salesperson-api.read', Id
FROM Clients
WHERE ClientId = 'swagger'