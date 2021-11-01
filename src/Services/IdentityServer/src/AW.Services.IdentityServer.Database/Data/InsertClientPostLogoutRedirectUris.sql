INSERT INTO ClientPostLogoutRedirectUris (PostLogoutRedirectUri, ClientId)
SELECT 'http://localhost:40610/signout-callback-oidc', Id
FROM Clients
WHERE ClientId = 'internal'