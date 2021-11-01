INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'http://localhost:61084/signin-oidc', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://dev.k8s-local.adventureworks.com/mvc-store/signin-oidc', Id
FROM Clients
WHERE ClientId = 'store'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'http://localhost:40610/mvc-internal/signin-oidc', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://dev.k8s-local.adventureworks.com/mvc-internal/signin-oidc', Id
FROM Clients
WHERE ClientId = 'internal'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'http://localhost:58093/customer-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://localhost:44331/basket-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'http://localhost:53154/product-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://k8s-local.adventureworks.com/product-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://k8s-local.adventureworks.com/basket-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'

INSERT INTO ClientRedirectUris (RedirectUri, ClientId)
SELECT 'https://dev.k8s-local.adventureworks.com/basket-api/oauth2-redirect.html', Id
FROM Clients
WHERE ClientId = 'swagger'