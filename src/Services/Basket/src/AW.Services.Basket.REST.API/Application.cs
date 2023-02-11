using AW.SharedKernel.Interfaces;

namespace AW.Services.Basket.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => typeof(Application).Namespace!;

        public string AppName => "Basket.REST.API";
    }
}