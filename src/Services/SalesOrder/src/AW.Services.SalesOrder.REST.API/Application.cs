using AW.SharedKernel.Interfaces;

namespace AW.Services.SalesOrder.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => "SalesOrder.REST.API";
    }
}