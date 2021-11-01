INSERT INTO ApiResourceClaims (ApiResourceId, Type)
SELECT Id, 'email'
FROM ApiResources
WHERE Name = 'basket-api'

INSERT INTO ApiResourceClaims (ApiResourceId, Type)
SELECT Id, 'profile'
FROM ApiResources
WHERE Name = 'basket-api'

INSERT INTO ApiResourceClaims (ApiResourceId, Type)
SELECT Id, 'name'
FROM ApiResources
WHERE Name = 'basket-api'