using Ardalis.Specification;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AW.Application.Autofac;
using AW.Application.Common.Behaviours;
using AW.Application.Customer.GetCustomers;
using AW.Persistence.EntityFramework;
using FluentValidation;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Web;

namespace AW.CustomerService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CustomerService>();
            builder.RegisterType<AWContext>();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();

            builder.AddMediatR(typeof(GetCustomersQuery).Assembly);
            builder.RegisterGeneric(typeof(RequestValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.AddAutoMapper(typeof(Global).Assembly, typeof(GetCustomersQuery).Assembly);

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