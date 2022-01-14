using AW.SharedKernel.Interfaces;

namespace AW.Services.Sales.SalesPerson.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => "SalesPerson.REST.API";
    }
}