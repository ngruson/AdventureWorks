using AW.Services.SharedKernel.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public class AWContextFactory : IAWContextFactory
    {
        public AWContext Create(
            string connectionString, 
            IMediator mediator, 
            Assembly configurationsAssembly
        )
        {
            var builder = new DbContextOptionsBuilder<AWContext>();
            builder
                .UseSqlServer(connectionString)
                .AddInterceptors(new AzureAdAuthenticationDbConnectionInterceptor());

            return new AWContext(
                builder.Options,
                mediator,
                configurationsAssembly
            );
        }
    }
}