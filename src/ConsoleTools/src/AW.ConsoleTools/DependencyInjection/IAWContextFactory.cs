using AW.Services.SharedKernel.EFCore;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public interface IAWContextFactory
    {
        AWContext Create(
            string connectionString,
            ILogger<AWContext> logger,
            IMediator mediator,
            Assembly configurationsAssembly
        );
    }
}