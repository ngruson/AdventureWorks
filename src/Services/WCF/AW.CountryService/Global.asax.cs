using Ardalis.Specification;
using Autofac;
using Autofac.Integration.Wcf;
using AW.Application.Country.ListCountries;
using AW.Persistence.EntityFramework;
using MediatR.Extensions.Autofac.DependencyInjection;
using System;
using System.Web;

namespace AW.CountryService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CountryService>();
            builder.RegisterType<AWContext>();

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();

            builder.AddMediatR(typeof(ListCountriesQuery).Assembly);

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