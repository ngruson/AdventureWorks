using Ardalis.Specification;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AutoMapper.EquivalencyExpression;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using AW.Services.Customer.Infrastructure.EF6;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.Validation;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace AW.Services.Customer.WCF
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CustomerService>();

            var sqlConnection = new SqlConnection
            {
                AccessToken = (new AzureServiceTokenProvider())
                    .GetAccessTokenAsync("https://database.windows.net").Result,
                ConnectionString = ConfigurationManager.ConnectionStrings["AWContext"].ConnectionString
            };
            builder.RegisterInstance(new AWContext(sqlConnection, true));

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterMediatR(typeof(GetCustomersQuery).Assembly);
            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterAutoMapper(c => c.AddCollectionMappers(), typeof(Global).Assembly, typeof(GetCustomersQuery).Assembly);

            builder.RegisterAssemblyTypes(typeof(GetCustomersQuery).Assembly)
                   .Where(t => t.Name.EndsWith("Validator"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();

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