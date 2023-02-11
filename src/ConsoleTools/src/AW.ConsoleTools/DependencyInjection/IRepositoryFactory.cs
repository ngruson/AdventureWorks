using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public interface IRepositoryFactory<T> where T : class, IAggregateRoot
    {
        IRepository<T> Create(
            ILogger<AWContext> logger,
            IAWContextFactory contextFactory,
            string connectionString,
            IMediator mediator,
            Assembly configurationsAssembly
        );
    }
}