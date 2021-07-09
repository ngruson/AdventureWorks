using Ardalis.Specification;
using Autofac;
using AW.Services.Product.Core.Handlers.GetProducts;
using System;
using System.Configuration;
using System.Data.SqlClient;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using Autofac.Integration.Wcf;
using AW.Services.Customer.Infrastructure.EF6;

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
            builder.RegisterInstance(new AWContext(sqlConnection, true));

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(GetProductsQuery).Assembly);
            builder.RegisterAutoMapper(typeof(GetProductsQuery).Assembly);

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