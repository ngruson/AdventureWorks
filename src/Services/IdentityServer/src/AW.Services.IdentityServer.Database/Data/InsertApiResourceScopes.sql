INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'basket-api.checkout', Id
FROM ApiResources
WHERE Name = 'basket-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'basket-api.write', Id
FROM ApiResources
WHERE Name = 'basket-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'basket-api.read', Id
FROM ApiResources
WHERE Name = 'basket-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'customer-api.read', Id
FROM ApiResources
WHERE Name = 'customer-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'product-api.read', Id
FROM ApiResources
WHERE Name = 'product-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'referencedata-api.read', Id
FROM ApiResources
WHERE Name = 'referencedata-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'salesorder-api.read', Id
FROM ApiResources
WHERE Name = 'salesorder-api'

INSERT INTO ApiResourceScopes (Scope, ApiResourceId)
SELECT 'salesperson-api.read', Id
FROM ApiResources
WHERE Name = 'salesperson-api'