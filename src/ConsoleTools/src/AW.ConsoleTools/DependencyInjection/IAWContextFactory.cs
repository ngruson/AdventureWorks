using AW.Services.SharedKernel.EFCore;
using MediatR;
using System.Reflection;

namespace AW.ConsoleTools.DependencyInjection
{
    public interface IAWContextFactory
    {
        AWContext Create(
            string connectionString,
            IMediator mediator,
            Assembly configurationsAssembly
        );
    }
}