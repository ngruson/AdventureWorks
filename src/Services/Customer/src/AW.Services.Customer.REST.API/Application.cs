using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace!;

        public string AppName => "Customer.REST.API";
    }
}