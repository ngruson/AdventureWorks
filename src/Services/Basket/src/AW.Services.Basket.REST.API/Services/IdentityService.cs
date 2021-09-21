using AW.Services.Basket.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace AW.Services.Basket.REST.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor context;

        public IdentityService(IHttpContextAccessor context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            return context.HttpContext.User.FindFirst("sub").Value;
        }

        public string GetUserName()
        {
            return context.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}