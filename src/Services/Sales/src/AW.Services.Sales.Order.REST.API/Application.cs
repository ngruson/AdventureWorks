using AW.SharedKernel.Interfaces;

namespace AW.Services.Sales.Order.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace!;

        public string AppName => "Sales.Order.REST.API";
    }
}