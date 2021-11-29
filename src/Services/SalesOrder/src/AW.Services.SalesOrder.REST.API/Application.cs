using AW.SharedKernel.Interfaces;

namespace AW.Services.SalesOrder.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => Namespace[(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1)..];
    }
}