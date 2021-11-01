INSERT INTO IdentityResources (Enabled, Name, DisplayName, Description, Required, Emphasize, ShowInDiscoveryDocument, Created, Updated, NonEditable)
VALUES (1, 'email', 'Your email address', NULL, 0, 1, 1, GETDATE(), NULL, 0)

INSERT INTO IdentityResources (Enabled, Name, DisplayName, Description, Required, Emphasize, ShowInDiscoveryDocument, Created, Updated, NonEditable)
VALUES (1, 'profile', 'User profile', 'Your user profile information (first name, last name, etc.)', , 0, 1, 1, GETDATE(), NULL, 0)

INSERT INTO IdentityResources (Enabled, Name, DisplayName, Description, Required, Emphasize, ShowInDiscoveryDocument, Created, Updated, NonEditable)
VALUES (1, 'openid', 'Your user identifier', NULL, 1, 0, 1, GETDATE(), NULL, 0)