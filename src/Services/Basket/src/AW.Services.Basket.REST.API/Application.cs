using AW.SharedKernel.Interfaces;
using System.Reflection;

namespace AW.Services.Basket.REST.API
{
    public class Application : IApplication
    {
        public string Namespace => Assembly.GetExecutingAssembly().GetName().Name;

        public string AppName => "Basket.REST.API";
    }
}