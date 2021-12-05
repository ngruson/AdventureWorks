using Ardalis.Specification;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.Services.SalesPerson.Infrastructure.EF6;
using AW.Services.SharedKernel.EF6;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace AW.Services.SalesPerson.WCF
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SalesPersonService>();

            var sqlConnection = new SqlConnection
            {
                AccessToken = (new AzureServiceTokenProvider())
                    .GetAccessTokenAsync("https://database.windows.net").Result,
                ConnectionString = ConfigurationManager.ConnectionStrings["AWContext"].ConnectionString
            };
            builder.RegisterInstance(new AWContext(
                sqlConnection, 
                true,
                typeof(EfRepository<>).Assembly,
                null
            ));

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(GetSalesPersonsQuery).Assembly);
            builder.RegisterAutoMapper(typeof(GetSalesPersonsQuery).Assembly);

            // Set the dependency resolver.
            var container = builder.Build();
            AutofacHostFactory.Container = container;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //Not used
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Not used
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //Not used
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Not used
        }

        protected void Session_End(object sender, EventArgs e)
        {
            //Not used
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //Not used
        }
    }
}