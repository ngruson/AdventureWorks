using AW.SharedKernel.Interfaces;

namespace AW.Services.Product.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace;

        public string AppName => "Product.REST.API";
    }
}