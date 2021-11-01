INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'email'
FROM IdentityResources
WHERE Name = 'email'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'email_verified'
FROM IdentityResources
WHERE Name = 'email'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'family_name'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'given_name'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'middle_name'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'nickname'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'preferred_username'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'profile'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'name'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'picture'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'gender'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'birthdate'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'zoneinfo'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'locale'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'updated_at'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'website'
FROM IdentityResources
WHERE Name = 'profile'

INSERT INTO IdentityResourceClaims (IdentityResourceId, Type)
SELECT Id, 'sub'
FROM IdentityResources
WHERE Name = 'openid'