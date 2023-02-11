// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace AW.Services.IdentityServer.Controllers.Account
{
    public class ExternalProvider
    {
        public ExternalProvider(string displayName, string authenticationScheme)
        {
            DisplayName = displayName;
            AuthenticationScheme = authenticationScheme;
        }

        public string DisplayName { get; private init; }
        public string AuthenticationScheme { get; private init; }
    }
}