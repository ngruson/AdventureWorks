﻿using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using AW.Application.Customer.GetCustomers;
using AW.Application.Interfaces;
using AW.Persistence.EntityFramework;
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
                .As(typeof(IAsyncRepository<>))
                .InstancePerLifetimeScope();

            builder.AddMediatR(typeof(GetCustomersQuery).Assembly);
            builder.AddAutoMapper(typeof(Global).Assembly, typeof(GetCustomersQuery).Assembly);

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