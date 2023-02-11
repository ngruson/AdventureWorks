// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AW.Services.IdentityServer.Controllers.Consent;

namespace AW.Services.IdentityServer.Controllers.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string? UserCode { get; set; }
    }
}