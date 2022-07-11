using AW.Services.IdentityServer.Core.Claims;
using AW.Services.IdentityServer.Core.Models;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AW.Services.IdentityServer.Core.Handlers.CreateLogin
{
    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand>
    {
        private readonly ILogger<CreateLoginCommandHandler> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public CreateLoginCommandHandler(
            ILogger<CreateLoginCommandHandler> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration) =>
                (this.logger, this.userManager, this.configuration) = (logger, userManager, configuration);

        public async Task<Unit> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var appUser = await userManager.FindByNameAsync(request.Username);
            if (appUser == null)
            {
                logger.LogInformation("Login for user {User} not found", request.Username);

                appUser = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                    EmailConfirmed = true
                };

                logger.LogInformation("Creating login for user {User}", request.Username);
                var result = await userManager.CreateAsync(appUser, configuration["DefaultUserPassword"]);

                if (result.Succeeded)
                {
                    logger.LogInformation("Creating claims for user {User}", request.Username);
                    await userManager.AddClaimsAsync(
                        appUser,
                        CreateClaims(request)
                    );
                }
            }
            else
                logger.LogInformation("Login for user {User} already exists", request.Username);

            return Unit.Value;
        }

        private static IEnumerable<Claim> CreateClaims(CreateLoginCommand request)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(request.Name?.FullName))
                claims.Add(new Claim(JwtClaimTypes.Name, request.Name.FullName));

            if (!string.IsNullOrEmpty(request.Name?.FirstName))
                claims.Add(new Claim(JwtClaimTypes.GivenName, request.Name.FirstName));

            if (!string.IsNullOrEmpty(request.Name?.MiddleName))
                claims.Add(new Claim(JwtClaimTypes.MiddleName, request.Name.MiddleName));

            if (!string.IsNullOrEmpty(request.Name?.LastName))
                claims.Add(new Claim(JwtClaimTypes.FamilyName, request.Name.LastName));

            if (!string.IsNullOrEmpty(request.CustomerNumber))
                claims.Add(new Claim(AwClaimTypes.CustomerNumber, request.CustomerNumber));

            return claims;
        }
    }
}