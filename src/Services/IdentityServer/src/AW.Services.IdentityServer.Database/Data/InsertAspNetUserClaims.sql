INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue)
SELECT Id, 'name', 'Nils Gruson'
FROM AspNetUsers
WHERE UserName = 'nils'

INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue)
SELECT Id, 'given_name', 'Nils'
FROM AspNetUsers
WHERE UserName = 'nils'

INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue)
SELECT Id, 'family_name', 'Gruson'
FROM AspNetUsers
WHERE UserName = 'nils'