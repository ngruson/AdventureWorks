using AW.Services.SharedKernel.EFCore;
using AW.Services.SharedKernel.Interfaces;
using MediatR;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public class RepositoryFactory<T> : IRepositoryFactory<T>
        where T : class, IAggregateRoot
    {
        public IRepository<T> Create(
            IAWContextFactory contextFactory,
            string connectionString,
            IMediator mediator,
            Assembly configurationsAssembly
        )
        {
            var dbContext = contextFactory.Create(
                connectionString,
                mediator,
                configurationsAssembly
            );

            return new EfRepository<T>(dbContext);
        }
    }
}