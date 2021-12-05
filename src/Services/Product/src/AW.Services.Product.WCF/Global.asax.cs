using Autofac;
using AW.Services.Product.Core.Handlers.GetProducts;
using System;
using System.Configuration;
using System.Data.SqlClient;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using Autofac.Integration.Wcf;
using AW.Services.SharedKernel.EF6;
using AW.Services.Product.Infrastructure.EF6;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Product.WCF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProductService>();

            var sqlConnection = new SqlConnection
            {
                //AccessToken = (new AzureServiceTokenProvider())
                //    .GetAccessTokenAsync("https://database.windows.net").Result,
                ConnectionString = ConfigurationManager.ConnectionStrings["AWContext"].ConnectionString
            };
            builder.RegisterInstance(new AWContext(
                sqlConnection,                 
                true,
                typeof(EfRepository<>).Assembly,
                null
            ));

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(GetProductsQuery).Assembly);
            builder.RegisterAutoMapper(typeof(GetProductsQuery).Assembly);

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