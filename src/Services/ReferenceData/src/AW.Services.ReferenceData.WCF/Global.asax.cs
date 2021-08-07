using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.ReferenceData.Infrastructure.EF6;
using AW.Services.SharedKernel.EF6;
using AW.SharedKernel.Interfaces;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace AW.Services.ReferenceData.WCF
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AddressTypeService>();

            var sqlConnection = new SqlConnection
            {
                AccessToken = (new AzureServiceTokenProvider())
                    .GetAccessTokenAsync("https://database.windows.net").Result,
                ConnectionString = ConfigurationManager.ConnectionStrings["AWContext"].ConnectionString
            };
            builder.RegisterInstance(new AWContext(
                sqlConnection, 
                true,
                typeof(EfRepository<>).Assembly
            ));

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(GetAddressTypesQuery).Assembly);
            builder.RegisterAutoMapper(typeof(Global).Assembly, typeof(GetAddressTypesQuery).Assembly);
            builder.RegisterType<LoggerFactory>()
                .As<ILoggerFactory>()
                .SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}