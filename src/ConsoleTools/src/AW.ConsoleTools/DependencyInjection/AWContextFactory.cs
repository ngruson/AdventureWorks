using AW.Services.SharedKernel.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public class AWContextFactory : IAWContextFactory
    {
        public AWContext Create(
            string connectionString, 
            ILogger<AWContext> logger,
            IMediator mediator, 
            Assembly configurationsAssembly
        )
        {
            var builder = new DbContextOptionsBuilder<AWContext>();
            builder
                .UseSqlServer(connectionString)
                .AddInterceptors(new AzureAdAuthenticationDbConnectionInterceptor());

            return new AWContext(
                logger,
                builder.Options,
                mediator,
                configurationsAssembly
            );
        }
    }
}