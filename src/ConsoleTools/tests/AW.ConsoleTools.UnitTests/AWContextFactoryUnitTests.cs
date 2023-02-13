using System.Reflection;
using AW.ConsoleTools.DependencyInjection;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.ConsoleTools.UnitTests
{
    public class AWContextFactoryUnitTests
    {
        public class AWContextFactoryCreate
        {
            [Theory, AutoMoqData]
            public void ReturnAWContext(
                AWContextFactory sut,
                string connectionString,
                Mock<ILogger<AWContext>> mockLogger,
                Mock<IMediator> mockMediator,
                Assembly configurationsAssembly
            )
            {
                // Act
                var context = sut.Create(
                    connectionString,
                    mockLogger.Object,
                    mockMediator.Object,
                    configurationsAssembly
                );

                // Assert
                context.Should().NotBeNull();
            }
        }
    }
}
