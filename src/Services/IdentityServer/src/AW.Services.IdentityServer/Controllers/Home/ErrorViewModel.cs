﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Duende.IdentityServer.Models;

namespace AW.Services.IdentityServer.Controllers.Home
{
    public class ErrorViewModel
    {
        public ErrorViewModel(ErrorMessage error)
        {
            Error = error;
        }

        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; private init; }
    }
}