using AW.Services.SharedKernel.Interfaces;
using MediatR;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public interface IRepositoryFactory<T> where T : class, IAggregateRoot
    {
        IRepository<T> Create(
            IAWContextFactory contextFactory,
            string connectionString,
            IMediator mediator,
            Assembly configurationsAssembly
        );
    }
}